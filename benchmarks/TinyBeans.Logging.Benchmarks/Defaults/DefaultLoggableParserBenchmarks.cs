using BenchmarkDotNet.Attributes;
using TinyBeans.Logging.Benchmarks.Dummy;
using TinyBeans.Logging.Defaults;

namespace TinyBeans.Logging.Benchmarks.Defaults {
    [SimpleJob]
    public class DefaultLoggableParserBenchmarks {
        private readonly DefaultLoggableParser _sut = new DefaultLoggableParser();
        private readonly DummyPoco _dummyPoco = new DummyPoco() { Property1 = "Hello", Property2 = "World", Property3 = "And JeffBot" };
        private readonly DummyPocoShouldLog _dummyPocoShouldLog = new DummyPocoShouldLog() { Property1 = "Hello", Property2 = "World", Property3 = "And JeffBot" };
        private readonly DummyPocoShouldLogSensitive _dummyPocoShouldLogSensitive = new DummyPocoShouldLogSensitive() { Property1 = "Hello", Property2 = "World", Property3 = "And JeffBot" };

        [Benchmark]
        public void NoAttribute() {
            _ = _sut.ParseLoggable(_dummyPoco);
        }
        [Benchmark]
        public void ShouldLog() {
            _ = _sut.ParseLoggable(_dummyPocoShouldLog);
        }
        [Benchmark]
        public void ShouldLogSensitive() {
            _ = _sut.ParseLoggable(_dummyPocoShouldLogSensitive);
        }
    }
}