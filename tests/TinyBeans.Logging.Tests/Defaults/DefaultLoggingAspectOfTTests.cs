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
        private readonly Mock<ILoggableParser> _loggableParserMock = new Mock<ILoggableParser>();
        private readonly Mock<IOptionsMonitor<LoggingOptions>> _loggingOptionsMock = new Mock<IOptionsMonitor<LoggingOptions>>();

        private readonly LoggingOptions _loggingOptions = new LoggingOptions();

        private DefaultLoggingAspect<DefaultLoggingAspectTests> Sut => new DefaultLoggingAspect<DefaultLoggingAspectTests>(_loggerMock.Object, _loggableParserMock.Object, _loggingOptionsMock.Object);

        public DefaultLoggingAspectTests() {
            _loggerMock.Setup(x => x.IsEnabled(LogLevel.Trace)).Returns(true);
            _loggerMock.Setup(x => x.IsEnabled(LogLevel.Debug)).Returns(true);
            _loggableParserMock.Setup(x => x.ParseLoggable(It.IsAny<object>())).Returns(new Dictionary<string, object>() { { "Key", "Value" } });
            _loggingOptionsMock.Setup(x => x.CurrentValue).Returns(_loggingOptions);
        }

        [Fact]
        public void Method0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(2));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(3));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(4));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(5));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void Method5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.Method, poco, poco, poco, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(6));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethod0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(2));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethod1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(3));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethod2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(4));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethod3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(5));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethod4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(6));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethod5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.Invoke(dummy.ResultMethod, poco, poco, poco, poco, poco);

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(7));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MethodAsync0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MethodAsync1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(2));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MethodAsync2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(3));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MethodAsync3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(4));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MethodAsync4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(5));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void MethodAsync5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.MethodAsync, poco, poco, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(6));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethodAsync0ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(2));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethodAsync1ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(3));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethodAsync2ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(4));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethodAsync3ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(5));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethodAsync4ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(6));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void ResultMethodAsync5ParametersRuns() {
            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(7));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void DoesNotExecuteStateScopesWhenTraceFalse() {
            _loggerMock.Setup(x => x.IsEnabled(LogLevel.Trace)).Returns(false);

            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(2));
        }

        [Fact]
        public void DoesNotExecuteLogsWhenDebugFalse() {
            _loggerMock.Setup(x => x.IsEnabled(LogLevel.Debug)).Returns(false);

            var sut = Sut;
            var dummy = new DummyClass();
            var poco = new DummyPocoShouldLog();

            sut.InvokeAsync(dummy.ResultMethodAsync, poco, poco, poco, poco, poco).Wait();

            _loggerMock.Verify(x => x.BeginScope(It.IsAny<It.IsAnyType>()), Times.Exactly(1));
            _loggerMock.Verify(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Exactly(0));
        }
    }
}