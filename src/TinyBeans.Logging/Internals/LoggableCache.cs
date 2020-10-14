using System;
using TinyBeans.Logging.Attributes;

namespace TinyBeans.Logging.Internals {
    internal class LoggableCache {
        public string Key { get; set; } = null!;
        public Delegate Lambda { get; set; } = null!;
        public OmitAttribute? Omit { get; set; }
        public ReplaceAttribute? Replace { get; set; }
    }
}