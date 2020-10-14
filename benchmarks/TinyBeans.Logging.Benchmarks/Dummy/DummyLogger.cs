using System;
using Microsoft.Extensions.Logging;

namespace TinyBeans.Logging.Benchmarks.Dummy {
    public class DummyLogger<T> : ILogger<T> {
        public IDisposable BeginScope<TState>(TState state) {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
        }
    }
}