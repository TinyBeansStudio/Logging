using Microsoft.Extensions.Logging;

namespace TinyBeans.Logging.Options {

    /// <summary>
    /// Options used by the <see cref="IAsyncLoggingAspect{T}"/>.
    /// </summary>
    public class LoggingAspectOptions {

        /// <summary>
        /// The message template to use when executing a method.
        /// Available template values are: 'assemblyName', 'className', and 'methodName'
        /// </summary>
        public string MethodExecutingTemplate { get; set; } = "Executing method '{methodName}' on class '{className}' in assembly {assemblyName}.";

        /// <summary>
        /// The message template to use when a method has executed.
        /// Available template values are: 'assemblyName', 'className', and 'methodName'
        /// </summary>
        public string MethodExecutedTemplate { get; set; } = "Executed method '{methodName}' on class '{className}' in assembly {assemblyName}.";

        /// <summary>
        /// The log level to write out executing and executed logs.
        /// </summary>
        public LogLevel ExecutionLogLevel { get; set; } = LogLevel.Debug;

        /// <summary>
        /// The message template to use for scopes.
        /// Available template values are: 'assemblyName', 'className', and 'methodName'
        /// </summary>
        public string ScopeTemplate { get; set; } = "{className}.{methodName} ({assemblyName})";
    }
}