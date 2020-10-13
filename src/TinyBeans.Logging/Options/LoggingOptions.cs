using Microsoft.Extensions.Logging;

namespace TinyBeans.Logging.Options {

    /// <summary>
    /// Options used by the <see cref="ILoggingAspect{T}"/>.
    /// </summary>
    public class LoggingOptions {

        /// <summary>
        /// The log level to write out executing and executed logs.
        /// </summary>
        public LogLevel ExecutionLogLevel { get; set; } = LogLevel.Debug;

        /// <summary>
        /// The message template to use when a method has executed.
        /// Available template values are: '{AssemblyName}', '{ClassName}', and '{MethodName}'
        /// </summary>
        public string MethodExecutedTemplate { get; set; } = "Executed method {MethodName} on class {ClassName} in assembly {AssemblyName}.";

        /// <summary>
        /// The message template to use when executing a method.
        /// Available template values are: '{AssemblyName}', '{ClassName}', and '{MethodName}'
        /// </summary>
        public string MethodExecutingTemplate { get; set; } = "Executing method {MethodName} on class {ClassName} in assembly {AssemblyName}.";

        /// <summary>
        /// The message template to use for scopes.
        /// Available template values are: '{AssemblyName}', '{ClassName}', and '{MethodName}'
        /// </summary>
        public string ScopeTemplate { get; set; } = "{ClassName}.{MethodName} ({AssemblyName})";

        /// <summary>
        /// The log level to write out state items as scopes.
        /// </summary>
        public LogLevel StateItemsLogLevel { get; set; } = LogLevel.Trace;
    }
}