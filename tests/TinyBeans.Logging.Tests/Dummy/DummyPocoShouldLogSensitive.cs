using TinyBeans.Logging.Attributes;

namespace TinyBeans.Logging.Tests.Dummy {
    [Loggable]
    internal class DummyPocoShouldLogSensitive {
        public string Property1 { get; set; }

        [Omit]
        public string Property2 { get; set; }

        [Replace("SENSITIVE")]
        public string Property3 { get; set; }
    }
}