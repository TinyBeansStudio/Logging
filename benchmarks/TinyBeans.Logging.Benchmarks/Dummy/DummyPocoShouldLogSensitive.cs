using TinyBeans.Logging.Attributes;

namespace TinyBeans.Logging.Benchmarks.Dummy {
    [ShouldLog]
    public class DummyPocoShouldLogSensitive {
        public string Property1 { get; set; }

        [Sensitive]
        public string Property2 { get; set; }

        [Sensitive(ReplacementValue = "SENSITIVE")]
        public string Property3 { get; set; }
    }
}