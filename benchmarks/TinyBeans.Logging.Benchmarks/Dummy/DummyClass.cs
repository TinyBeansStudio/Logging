namespace TinyBeans.Logging.Benchmarks.Dummy {
    public class DummyClass {
        public DummyPocoShouldLogSensitive ResultMethod(DummyPocoShouldLog dummyPoco1, DummyPocoShouldLog dummyPoco2, DummyPocoShouldLog dummyPoco3, DummyPocoShouldLog dummyPoco4, DummyPocoShouldLog dummyPoco5) {
            return new DummyPocoShouldLogSensitive() {
                Property1 = "Hello",
                Property2 = "World",
                Property3 = "And JeffBot"
            };
        }
    }
}