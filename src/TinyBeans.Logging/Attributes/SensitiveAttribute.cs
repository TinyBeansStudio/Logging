using System;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate the property or field is sensitive.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SensitiveAttribute : Attribute {

        /// <summary>
        /// The value to replace the sensitive property or field with. Setting to null will use the default value.
        /// </summary>
        public object? ReplacementValue { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="replacementValue">The value to replace the sensitive property or field with. Setting to null will use the default value.</param>
        public SensitiveAttribute(object? replacementValue = null) {
            ReplacementValue = replacementValue;
        }
    }
}