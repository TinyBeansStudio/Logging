using BenchmarkDotNet.Running;
using TinyBeans.Logging.Benchmarks.Defaults;

namespace TinyBeans.Logging.Benchmarks {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<DefaultLoggableParserBenchmarks>();
        }
    }
}