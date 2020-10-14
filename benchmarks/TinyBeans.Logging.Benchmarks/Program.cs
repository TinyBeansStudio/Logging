using BenchmarkDotNet.Running;

namespace TinyBeans.Logging.Benchmarks {
    internal class Program {
        private static void Main(string[] args) {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}