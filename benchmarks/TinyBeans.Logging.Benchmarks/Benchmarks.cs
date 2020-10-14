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
        private readonly DefaultLoggableParser _loggableParser = new DefaultLoggableParser();
        private readonly DefaultLoggingTemplateHelper _loggingTemplateHelper = new DefaultLoggingTemplateHelper();
        private readonly DummyClass _dummyClass = new DummyClass();
        private readonly DummyLogger<Benchmarks> _dummyLogger = new DummyLogger<Benchmarks>();
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

            _loggingAspect = new DefaultLoggingAspect<Benchmarks>(_dummyLogger, _loggableParser, _loggingTemplateHelper, options);
        }

        [Benchmark]
        public void OrderExecutingTemplate() {
            _loggingTemplateHelper.OrderNames(_options.MethodExecutingTemplate, "Hello", "World", "And JeffBot");
        }

        [Benchmark]
        public void OrderScopeTemplate() {
            _loggingTemplateHelper.OrderNames(_options.ScopeTemplate, "Hello", "World", "And JeffBot");
        }

        [Benchmark]
        public void OrderExecutedTemplate() {
            _loggingTemplateHelper.OrderNames(_options.MethodExecutedTemplate, "Hello", "World", "And JeffBot");
        }

        [Benchmark]
        public void NoAttribute() {
            _ = _loggableParser.ParseLoggable(_dummyPoco);
        }

        [Benchmark]
        public void ShouldLog() {
            _ = _loggableParser.ParseLoggable(_dummyPocoShouldLog);
        }

        [Benchmark]
        public void ShouldLogSensitive() {
            _ = _loggableParser.ParseLoggable(_dummyPocoShouldLogSensitive);
        }

        [Benchmark]
        public void Full() {
            _loggingAspect.Invoke(_dummyClass.ResultMethod, _dummyPocoShouldLog, _dummyPocoShouldLog, _dummyPocoShouldLog, _dummyPocoShouldLog, _dummyPocoShouldLog);
        }
    }
}