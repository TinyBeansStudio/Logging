using System;
using TinyBeans.Logging.Models;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// Default implementation of <see cref="ILoggingTemplateHelper"/>
    /// </summary>
    public class DefaultLoggingTemplateHelper : ILoggingTemplateHelper {
        private static readonly int[][] _templateOrders = new int[][] {
            new int[] { -1, -1, -1 },
            new int[] { -1, -1, -1 },
            new int[] { -1, -1, -1 }
        };

        /// <summary>
        /// Logging template field representing the name of the assembly.
        /// </summary>
        public string AssemblyName { get; } = "{AssemblyName}";

        /// <summary>
        /// Logging template field representing the name of the class.
        /// </summary>
        public string ClassName { get; } = "{ClassName}";

        /// <summary>
        /// Logging template field representing the name of the method.
        /// </summary>
        public string MethodName { get; } = "{MethodName}";

        /// <summary>
        /// Orders the assembly, class, and method names based on the order of the template.
        /// </summary>
        /// <param name="template">The template to base the order on.</param>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="className">The name of the class.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A string array with the names in the proper order.</returns>
        public string[] OrderNames(Template template, string assemblyName, string className, string methodName) {
            if (_templateOrders[template.Id][0] == -1) {
                lock (_templateOrders) {
                    if (_templateOrders[template.Id][0] == -1) {
                        var span = template.Value.AsSpan();

                        _templateOrders[template.Id][0] = span.IndexOf(AssemblyName.AsSpan());
                        _templateOrders[template.Id][1] = span.IndexOf(ClassName.AsSpan());
                        _templateOrders[template.Id][2] = span.IndexOf(MethodName.AsSpan());
                    }
                }
            }

            var order = _templateOrders[template.Id];

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