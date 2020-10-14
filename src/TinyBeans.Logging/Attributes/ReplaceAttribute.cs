using System;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate the property should be replaced.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ReplaceAttribute : Attribute {

        /// <summary>
        /// The value to replace the original.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value to replace the original.</param>
        public ReplaceAttribute(object value) {
            Value = value;
        }
    }
}