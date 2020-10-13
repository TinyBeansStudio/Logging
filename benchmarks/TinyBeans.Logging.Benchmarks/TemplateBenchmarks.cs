using BenchmarkDotNet.Attributes;
using TinyBeans.Logging.Options;

namespace TinyBeans.Logging.Benchmarks {

    [SimpleJob]
    public class TemplateBenchmarks {
        private readonly LoggingAspectOptions _options = new LoggingAspectOptions();

        [Benchmark]
        public void OrderNames() {
            Template.OrderNames(_options.MethodExecutingTemplate, "Hello", "World", "And JeffBot");

            Template.OrderNames(_options.ScopeTemplate, "Hello", "World", "And JeffBot");

            Template.OrderNames(_options.MethodExecutedTemplate, "Hello", "World", "And JeffBot");
        }
    }
}