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

        /// <summary>
        /// Used to parse the loggable items from a state object.
        /// </summary>
        /// <typeparam name="TState">The type of object to parse the loggable items from.</typeparam>
        /// <param name="state">The object to parse the loggable items from.</param>
        /// <returns>A dictionary containing the parsed loggable items.</returns>
        public Dictionary<string, object> ParseLoggableItems<TState>(TState state) {
            var type = typeof(TState);

            var items = _typeCache
                .GetOrAdd(type, key => {
                    var shouldLogAttribute = key.GetTypeInfo().GetCustomAttribute<ShouldLogAttribute>(false);
                    if (shouldLogAttribute is null) {
                        return Array.Empty<(PropertyInfo property, SensitiveAttribute? sensitive)>();
                    }

                    return key.GetTypeInfo().GetProperties().Select(x => (x, (SensitiveAttribute?)x.GetCustomAttribute<SensitiveAttribute>(false))).ToArray();
                });

            return items
                .ToDictionary(x => $"{type.Name}.{x.property.Name}", x => {
                    if (x.sensitive is null) {
                        return x.property.GetValue(state);
                    }

                    if (x.sensitive.ReplacementValue is object) {
                        return x.sensitive.ReplacementValue;
                    }

                    if (x.property.PropertyType.IsValueType) {
                        return Activator.CreateInstance(x.property.PropertyType);
                    }

                    return null;
                })
                .Where(x => x.Value is object)
                .ToDictionary(x => x.Key, x => x.Value!);
        }
    }
}