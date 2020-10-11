using System;
using Microsoft.Extensions.Logging;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate an <see cref="ILoggingAspect{T}"/> parameter or result should be logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ShouldLogAttribute : Attribute {

        /// <summary>
        /// The <see cref="Microsoft.Extensions.Logging.LogLevel"/> which parameters and results should be logged.
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }
}