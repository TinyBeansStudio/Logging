using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TinyBeans.Logging.Abstractions;
using TinyBeans.Logging.Attributes;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementation of <see cref="ILoggableParser"/>.
    /// </summary>
    public class DefaultLoggableParser : ILoggableParser {
        private static readonly ConcurrentDictionary<Type, List<(Delegate method, string name, SensitiveAttribute? sensitive)>> _typeCache = new ConcurrentDictionary<Type, List<(Delegate method, string name, SensitiveAttribute? sensitive)>>();
        private static readonly Dictionary<string, object> _emptyDictionary = new Dictionary<string, object>();

        /// <summary>
        /// Used to parse loggable items from a state object.
        /// </summary>
        /// <typeparam name="TState">The type of object to parse the loggable items.</typeparam>
        /// <param name="state">The object to parse the loggable items.</param>
        /// <returns>A dictionary containing the parsed loggable items.</returns>
        public IEnumerable<KeyValuePair<string, object>> ParseLoggable<TState>(TState state) {
            if (state is null) {
                return _emptyDictionary;
            }

            var type = state.GetType();

            var items = _typeCache
                .GetOrAdd(type, key => {
                    var shouldLogAttribute = key.GetTypeInfo().GetCustomAttribute<LoggableAttribute>(false);
                    if (shouldLogAttribute is null) {
                        return new List<(Delegate method, string name, SensitiveAttribute? sensitive)>();
                    }

                    return key.GetTypeInfo().GetProperties().Select(x => {
                        var parameter = Expression.Parameter(type, "obj");
                        var property = Expression.Property(parameter, x.Name);
                        var convert = Expression.Convert(property, typeof(object));
                        var method = Expression.Lambda(typeof(Func<TState, object>), convert, parameter).Compile();

                        return (method, x.Name, (SensitiveAttribute?)x.GetCustomAttribute<SensitiveAttribute>(true));
                    }).ToList();
                });

            if (items.Count() == 0) {
                return _emptyDictionary;
            }

            var loggables = new List<KeyValuePair<string, object>>(items.Count());

            foreach (var item in items) {
                object value;
                if (item.sensitive is null) {
                    value = ((Func<TState, object>)item.method)(state);
                } else if (item.sensitive.ReplacementValue is object) {
                    value = item.sensitive.ReplacementValue;
                } else {
                    value = null!;
                }

                if (value is object) {
                    loggables.Add(new KeyValuePair<string, object>($"{type.Name}_{item.name}", value));
                }
            }

            return loggables;
        }
    }
}