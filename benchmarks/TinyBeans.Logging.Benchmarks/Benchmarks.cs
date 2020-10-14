using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TinyBeans.Logging.Benchmarks.Dummy;
using TinyBeans.Logging.Defaults;
using TinyBeans.Logging.Options;

namespace TinyBeans.Logging.Benchmarks {
    [SimpleJob]
    [MemoryDiagnoser]
    public class Benchmarks {
        private readonly DefaultLoggableParser _sut = new DefaultLoggableParser();
        private readonly DummyClass _dummyClass = new DummyClass();
        private readonly DummyPoco _dummyPoco = new DummyPoco() { Property1 = "Hello", Property2 = "World", Property3 = "And JeffBot" };
        private readonly DummyPocoShouldLog _dummyPocoShouldLog = new DummyPocoShouldLog() { Property1 = "Hello", Property2 = "World", Property3 = "And JeffBot" };
        private readonly DummyPocoShouldLogSensitive _dummyPocoShouldLogSensitive = new DummyPocoShouldLogSensitive() { Property1 = "Hello", Property2 = "World", Property3 = "And JeffBot" };
        private readonly DefaultLoggingAspect<Benchmarks> _loggingAspect;
        private readonly LoggingOptions _options;

        public Benchmarks() {
            var services = new ServiceCollection();
            services.Configure<LoggingOptions>((options) => { });

            var builder = services.BuildServiceProvider();
            var options = builder.GetRequiredService<IOptionsMonitor<LoggingOptions>>();
            _options = options.CurrentValue;

            _loggingAspect = new DefaultLoggingAspect<Benchmarks>(new DummyLogger<Benchmarks>(), new DefaultLoggableParser(), new DefaultLoggingTemplateHelper(), options);
        }

        // [Benchmark]
        // public void OrderExecutingTemplate() {
        //     Constants.OrderNames(_options._methodExecutingTemplate, "Hello", "World", "And JeffBot");
        // }
        //
        // [Benchmark]
        // public void OrderScopeTemplate() {
        //     Constants.OrderNames(_options._scopeTemplate, "Hello", "World", "And JeffBot");
        // }
        //
        // [Benchmark]
        // public void OrderExecutedTemplate() {
        //     Constants.OrderNames(_options._methodExecutedTemplate, "Hello", "World", "And JeffBot");
        // }
        //
        // [Benchmark]
        // public void NoAttribute() {
        //     _ = _sut.ParseLoggable(_dummyPoco);
        // }
        //
        // [Benchmark]
        // public void ShouldLog() {
        //     _ = _sut.ParseLoggable(_dummyPocoShouldLog);
        // }
        //
        // [Benchmark]
        // public void ShouldLogSensitive() {
        //     _ = _sut.ParseLoggable(_dummyPocoShouldLogSensitive);
        // }

        [Benchmark]
        public void Full() {
            _loggingAspect.Invoke(_dummyClass.ResultMethod, _dummyPocoShouldLog, _dummyPocoShouldLog, _dummyPocoShouldLog, _dummyPocoShouldLog, _dummyPocoShouldLog);
        }
    }
}