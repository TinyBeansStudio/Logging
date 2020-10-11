using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TinyBeans.Logging.Abstractions;
using TinyBeans.Logging.Attributes;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementatin of <see cref="ILoggableStateParser"/>.
    /// </summary>
    public class DefaultLoggableStateParser : ILoggableStateParser {
        private static readonly ConcurrentDictionary<Type, (PropertyInfo property, SensitiveAttribute? sensitive)[]> _typeCache = new ConcurrentDictionary<Type, (PropertyInfo property, SensitiveAttribute? sensitive)[]>();
        private static readonly Dictionary<string, object> _emptyDictionary = new Dictionary<string, object>();

        /// <summary>
        /// Used to parse the loggable items from a state object.
        /// </summary>
        /// <typeparam name="TState">The type of object to parse the loggable items from.</typeparam>
        /// <param name="state">The object to parse the loggable items from.</param>
        /// <returns>A dictionary containing the parsed loggable items.</returns>
        public Dictionary<string, object> ParseLoggableItems<TState>(TState state) {
            if (state is null) {
                return _emptyDictionary;
            }

            var type = state.GetType();

            var items = _typeCache
                .GetOrAdd(type, key => {
                    var shouldLogAttribute = key.GetTypeInfo().GetCustomAttribute<ShouldLogAttribute>(false);
                    if (shouldLogAttribute is null) {
                        return Array.Empty<(PropertyInfo property, SensitiveAttribute? sensitive)>();
                    }

                    return key.GetTypeInfo().GetProperties().Select(x => (x, (SensitiveAttribute?)x.GetCustomAttribute<SensitiveAttribute>(true))).ToArray();
                });

            if (items.Count() == 0) {
                return _emptyDictionary;
            }

            var loggables = new Dictionary<string, object>(items.Count());

            foreach (var item in items) {
                object value;
                if (item.sensitive is null) {
                    value = item.property.GetValue(state);
                } else if (item.sensitive.ReplacementValue is object) {
                    value = item.sensitive.ReplacementValue;
                } else if (item.property.PropertyType.IsValueType) {
                    value = Activator.CreateInstance(item.property.PropertyType);
                } else {
                    value = null!;
                }

                if (value is object) {
                    loggables.Add($"{type.Name}_{item.property.Name}", value);
                }
            }

            return loggables;
        }
    }
}