using System;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate the property is sensitive.
    /// </summary>
    [Obsolete("Replaced with OmitAttribute and ReplaceAttribute", true)]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SensitiveAttribute : Attribute {

        /// <summary>
        /// The value to replace the sensitive property. Setting to null will omit the value.
        /// </summary>
        public object? ReplacementValue { get; set; }
    }
}