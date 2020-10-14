using TinyBeans.Logging.Models;

namespace TinyBeans.Logging {

    /// <summary>
    /// Represents a type used to aid in logging templates.
    /// </summary>
    public interface ILoggingTemplateHelper {

        /// <summary>
        /// Logging template field representing the name of the assembly.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// Logging template field representing the name of the class.
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Logging template field representing the name of the method.
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Orders the assembly, class, and method names based on the order of the template.
        /// </summary>
        /// <param name="template">The template to base the order on.</param>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="className">The name of the class.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>A string array with the names in the proper order.</returns>
        string[] OrderNames(Template template, string assemblyName, string className, string methodName);
    }
}