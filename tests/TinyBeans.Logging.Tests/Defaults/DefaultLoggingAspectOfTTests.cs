using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TinyBeans.Logging.Abstractions;
using TinyBeans.Logging.Defaults;
using TinyBeans.Logging.Options;
using TinyBeans.Logging.Tests.Dummy;
using Xunit;

namespace TinyBeans.Logging.Tests.Defaults {
    public class DefaultLoggingAspectTests {
        private readonly Mock<ILogger<DefaultLoggingAspectTests>> _loggerMock = new Mock<ILogger<DefaultLoggingAspectTests>>();
        private readonly Mock<ILoggableStateParser> _loggableStateParserMock = new Mock<ILoggableStateParser>();
        private readonly Mock<IOptionsMonitor<LoggingAspectOptions>> _loggingAspectOptionsMock = new Mock<IOptionsMonitor<LoggingAspectOptions>>();

        private readonly LoggingAspectOptions _loggingAspectOptions = new LoggingAspectOptions();

        private DefaultLoggingAspect<DefaultLoggingAspectTests> Sut => new DefaultLoggingAspect<DefaultLoggingAspectTests>(_loggerMock.Object, _loggableStateParserMock.Object, _loggingAspectOptionsMock.Object);

        public DefaultLoggingAspectTests() {
            _loggerMock.Setup(x => x.IsEnabled(It.IsAny<LogLevel>())).Returns(true);
            _loggableStateParserMock.Setup(x => x.ParseLoggableItems(It.IsAny<object>())).Returns(new Dictionary<string, object>());
            _loggingAspectOptionsMock.Setup(x => x.CurrentValue).Returns(_loggingAspectOptions);
        }

        [Fact]
        public void Method0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Once);
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Once);
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco);
        }

        [Fact]
        public void Method3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco, poco);
        }

        [Fact]
        public void Method4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco, poco, poco);
        }

        [Fact]
        public void Method5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco, poco, poco, poco);
        }

        [Fact]
        public void ResultMethod0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod);
        }

        [Fact]
        public void ResultMethod1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco);
        }

        [Fact]
        public void ResultMethod2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco);
        }

        [Fact]
        public void ResultMethod3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco, poco);
        }

        [Fact]
        public void ResultMethod4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco, poco, poco);
        }

        [Fact]
        public void ResultMethod5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco, poco, poco, poco);
        }

        [Fact]
        public void MethodAsync0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync).Wait();
        }

        [Fact]
        public void MethodAsync1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco).Wait();
        }

        [Fact]
        public void MethodAsync2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco).Wait();
        }

        [Fact]
        public void MethodAsync3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco, poco).Wait();
        }

        [Fact]
        public void MethodAsync4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco, poco, poco).Wait();
        }

        [Fact]
        public void MethodAsync5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco, poco, poco, poco).Wait();
        }

        [Fact]
        public void ResultMethodAsync0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync).Wait();
        }

        [Fact]
        public void ResultMethodAsync1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco).Wait();
        }

        [Fact]
        public void ResultMethodAsync2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco).Wait();
        }

        [Fact]
        public void ResultMethodAsync3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco).Wait();
        }

        [Fact]
        public void ResultMethodAsync4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco, poco).Wait();
        }

        [Fact]
        public void ResultMethodAsync5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco, poco, poco).Wait();
        }
    }
}