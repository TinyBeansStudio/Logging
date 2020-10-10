using System;
using Microsoft.Extensions.Logging;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate an <see cref="IAsyncLoggingAspect{T}"/> parameter or result should be logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ShouldLogAttribute : Attribute {

        /// <summary>
        /// The <see cref="Microsoft.Extensions.Logging.LogLevel"/> which parameters and results should be logged.
        /// </summary>
        public LogLevel LogLevel { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logLevel">The <see cref="Microsoft.Extensions.Logging.LogLevel"/> which parameters and results should be logged.</param>
        public ShouldLogAttribute(LogLevel logLevel = LogLevel.Trace) {
            LogLevel = logLevel;
        }
    }
}