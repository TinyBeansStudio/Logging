using Microsoft.Extensions.Logging;
using TinyBeans.Logging.Models;

namespace TinyBeans.Logging.Options {

    /// <summary>
    /// Options used by the <see cref="ILoggingAspect{T}"/>.
    /// </summary>
    public class LoggingOptions {
        private const int _methodExecutedTemplateId = 0;
        private const int _methodExecutingTemplateId = 1;
        private const int _scopeTemplateId = 2;

        /// <summary>
        /// The log level to write out executing and executed logs.
        /// </summary>
        public LogLevel ExecutionLogLevel { get; set; } = LogLevel.Debug;

        /// <summary>
        /// The <see cref="Template"/> used for method executed logs.
        /// </summary>
        public Template MethodExecutedTemplate { get; private set; } = new Template(_methodExecutedTemplateId, "Executed method {MethodName} on class {ClassName} in assembly {AssemblyName}.");

        /// <summary>
        /// The message template to use when a method has executed.
        /// Available template values are: '{AssemblyName}', '{ClassName}', and '{MethodName}'
        /// </summary>
        public string MethodExecutedTemplateValue {
            get {
                return MethodExecutedTemplate.Value;
            }
            set {
                MethodExecutedTemplate = new Template(_methodExecutedTemplateId, value);
            }
        }

        /// <summary>
        /// The <see cref="Template"/> used for method executing logs.
        /// </summary>
        public Template MethodExecutingTemplate { get; private set; } = new Template(_methodExecutingTemplateId, "Executing method {MethodName} on class {ClassName} in assembly {AssemblyName}.");

        /// <summary>
        /// The message template to use when executing a method.
        /// Available template values are: '{AssemblyName}', '{ClassName}', and '{MethodName}'
        /// </summary>
        public string MethodExecutingTemplateValue {
            get {
                return MethodExecutingTemplate.Value;
            }
            set {
                MethodExecutingTemplate = new Template(_methodExecutingTemplateId, value);
            }
        }

        /// <summary>
        /// The <see cref="Template"/> used for scope logs.
        /// </summary>
        public Template ScopeTemplate { get; private set; } = new Template(_scopeTemplateId, "{ClassName}.{MethodName} ({AssemblyName})");

        /// <summary>
        /// The message template to use for scopes.
        /// Available template values are: '{AssemblyName}', '{ClassName}', and '{MethodName}'
        /// </summary>
        public string ScopeTemplateValue {
            get {
                return ScopeTemplate.Value;
            }
            set {
                ScopeTemplate = new Template(_scopeTemplateId, value);
            }
        }

        /// <summary>
        /// The log level to write out state items as scopes.
        /// </summary>
        public LogLevel StateItemsLogLevel { get; set; } = LogLevel.Trace;
    }
}