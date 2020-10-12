using System;
using Microsoft.Extensions.Logging;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate the properties an <see cref="ILoggingAspect{T}"/> parameter or result should be logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class LoggableAttribute : Attribute {
    }
}