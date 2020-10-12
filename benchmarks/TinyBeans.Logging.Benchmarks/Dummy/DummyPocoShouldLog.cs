using TinyBeans.Logging.Attributes;

namespace TinyBeans.Logging.Benchmarks.Dummy {
    [Loggable]
    public class DummyPocoShouldLog {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
    }
}