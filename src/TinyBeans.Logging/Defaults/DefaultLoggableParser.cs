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
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private class Cache {
            public string Key { get; set; }
            public Delegate Lambda { get; set; }
            public OmitAttribute? Omit { get; set; }
            public ReplaceAttribute? Replace { get; set; }
        }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        private static readonly ConcurrentDictionary<Type, Cache[]> _typeCache = new ConcurrentDictionary<Type, Cache[]>();
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
                if (item.Omit is object) {
                    // Ignore
                } else if (item.Replace is object) {
                    loggables.Add(new KeyValuePair<string, object>(item.Key, item.Replace.Value));
                } else if (((Func<TState, object>)item.Lambda)(state) is object value) {
                    loggables.Add(new KeyValuePair<string, object>(item.Key, value));
                }
            }
            
            return loggables;
        }

        private static Cache[] GetCache<TState>(Type stateType) {
            var shouldLogAttribute = stateType.GetTypeInfo().GetCustomAttribute<LoggableAttribute>(false);
            if (shouldLogAttribute is null) {
                return Array.Empty<Cache>();
            }

            return stateType.GetTypeInfo().GetProperties().Select(propertyInfo => {
                var parameter = Expression.Parameter(stateType, "obj");
                var property = Expression.Property(parameter, propertyInfo.Name);
                var convert = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda(typeof(Func<TState, object>), convert, parameter).Compile();

                return new Cache() {
                    Key = $"{stateType.Name}_{propertyInfo.Name}",
                    Lambda = lambda,
                    Omit = propertyInfo.GetCustomAttribute<OmitAttribute>(false),
                    Replace = propertyInfo.GetCustomAttribute<ReplaceAttribute>(false)
                };
            }).ToArray();
        }
    }
}

/*

List<Cache>
|             Method |      Mean |    Error |   StdDev |
|------------------- |----------:|---------:|---------:|
|        NoAttribute |  41.47 ns | 0.742 ns | 0.579 ns |
|          ShouldLog | 226.76 ns | 3.871 ns | 3.621 ns |
| ShouldLogSensitive | 194.50 ns | 2.975 ns | 2.783 ns |

Cache[]
|             Method |      Mean |    Error |   StdDev |
|------------------- |----------:|---------:|---------:|
|        NoAttribute |  24.95 ns | 0.469 ns | 0.439 ns |
|          ShouldLog | 191.48 ns | 2.929 ns | 2.597 ns |
| ShouldLogSensitive | 152.29 ns | 2.877 ns | 2.691 ns |

Changing from property name to full key in cache
|             Method |     Mean |    Error |   StdDev |
|------------------- |---------:|---------:|---------:|
|        NoAttribute | 26.27 ns | 0.528 ns | 0.607 ns |
|          ShouldLog | 64.09 ns | 0.494 ns | 0.413 ns |
| ShouldLogSensitive | 57.53 ns | 0.401 ns | 0.355 ns |
*/