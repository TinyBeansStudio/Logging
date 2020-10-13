using System;
using System.Collections.Concurrent;

namespace TinyBeans.Logging {

    /// <summary>
    /// Static class with template aiding information.
    /// </summary>
    public static class Template {
        private static readonly ConcurrentDictionary<string, int[]> _templateOrders = new ConcurrentDictionary<string, int[]>();

        /// <summary>
        /// Template representing the name of the assembly.
        /// </summary>
        public static readonly string AssemblyName = "{AssemblyName}";

        /// <summary>
        /// Template representing the name of the class.
        /// </summary>
        public static readonly string ClassName = "{ClassName}";

        /// <summary>
        /// Template representing the name of the method.
        /// </summary>
        public static readonly string MethodName = "{MethodName}";

        /// <summary>
        /// Takes the assembly, class, and method names and orders them based on the supplied template.
        /// </summary>
        /// <param name="template">The template with the <see cref="AssemblyName"/>, <see cref="ClassName"/>, and <see cref="MethodName"/>.</param>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="className">The name of the class.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A string array with the <see cref="AssemblyName"/>, <see cref="ClassName"/>, and <see cref="MethodName"/> in template order.</returns>
        public static string[] OrderNames(string template, string assemblyName, string className, string methodName) {
            var order = _templateOrders
                .GetOrAdd(template, key => {
                    return new int[3] {
                        key.IndexOf(AssemblyName, StringComparison.OrdinalIgnoreCase),
                        key.IndexOf(ClassName, StringComparison.OrdinalIgnoreCase),
                        key.IndexOf(MethodName, StringComparison.OrdinalIgnoreCase)
                    };
                });

            var names = new string[3];
            if (order[0] < order[1] && order[0] < order[2]) {
                names[0] = assemblyName;

                if (order[1] < order[2]) {
                    names[1] = className;
                    names[2] = methodName;
                } else {
                    names[1] = methodName;
                    names[2] = className;
                }
            } else if (order[1] < order[0] && order[1] < order[2]) {
                names[0] = className;

                if (order[0] < order[2]) {
                    names[1] = assemblyName;
                    names[2] = methodName;
                } else {
                    names[1] = methodName;
                    names[2] = assemblyName;
                }
            } else {
                names[0] = methodName;

                if (order[0] < order[1]) {
                    names[1] = assemblyName;
                    names[2] = className;
                } else {
                    names[1] = className;
                    names[2] = assemblyName;
                }
            }

            return names;
        }
    }
}