using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TinyBeans.Logging.Attributes;
using TinyBeans.Logging.Internals;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementation of <see cref="ILoggableParser"/>.
    /// </summary>
    public class DefaultLoggableParser : ILoggableParser {
        private static readonly ConcurrentDictionary<Type, LoggableCache[]> _typeCache = new ConcurrentDictionary<Type, LoggableCache[]>();
        private static readonly KeyValuePair<string, object>[] _emptyItems = Array.Empty<KeyValuePair<string, object>>();

        /// <summary>
        /// Used to parse loggable items from a state object.
        /// </summary>
        /// <typeparam name="TState">The type of object to parse the loggable items.</typeparam>
        /// <param name="state">The object to parse the loggable items.</param>
        /// <returns>A dictionary containing the parsed loggable items.</returns>
        public IEnumerable<KeyValuePair<string, object>> ParseLoggable<TState>(TState state) {
            if (state is null) {
                return _emptyItems;
            }

            var type = state.GetType();
            var items = _typeCache
                .GetOrAdd(type, key => {
                    return GetCache<TState>(key);
                });

            if (items.Length == 0) {
                return _emptyItems;
            }

            var loggables = new List<KeyValuePair<string, object>>(items.Length);

            foreach (var item in items) {
                if (item.Replace is object) {
                    loggables.Add(new KeyValuePair<string, object>(item.Key, item.Replace.Value));
                } else if (item.Omit is null && ((Func<TState, object>)item.Lambda)(state) is object value) {
                    loggables.Add(new KeyValuePair<string, object>(item.Key, value));
                }
            }

            return loggables;
        }

        private static LoggableCache[] GetCache<TState>(Type stateType) {
            var shouldLogAttribute = stateType.GetTypeInfo().GetCustomAttribute<LoggableAttribute>(false);
            if (shouldLogAttribute is null) {
                return Array.Empty<LoggableCache>();
            }

            return stateType.GetTypeInfo().GetProperties().Select(propertyInfo => {
                var parameter = Expression.Parameter(stateType, "state");
                var property = Expression.Property(parameter, propertyInfo.Name);
                var convert = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda(typeof(Func<TState, object>), convert, parameter).Compile();

                return new LoggableCache() {
                    Key = $"{stateType.Name}_{propertyInfo.Name}",
                    Lambda = lambda,
                    Omit = propertyInfo.GetCustomAttribute<OmitAttribute>(false),
                    Replace = propertyInfo.GetCustomAttribute<ReplaceAttribute>(false)
                };
            }).ToArray();
        }
    }
}