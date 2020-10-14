using System;

namespace TinyBeans.Logging.Attributes {

    /// <summary>
    /// Attribute to indicate the property should be omitted.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class OmitAttribute : Attribute {
    }
}