using System.Linq;
using TinyBeans.Logging.Defaults;
using TinyBeans.Logging.Tests.Dummy;
using Xunit;

namespace TinyBeans.Logging.Tests.Defaults {
    public class DefaultLoggableParserTests {
        private DefaultLoggableParser Sut => new DefaultLoggableParser();

        [Fact]
        public void NoAttributes() {
            var dummy = new DummyPoco();
            var items = Sut.ParseLoggable(dummy);

            Assert.Empty(items);
        }

        [Fact]
        public void ParsesProperties() {
            var dummy = new DummyPocoShouldLog() {
                Property1 = "Hello",
                Property2 = "World"
            };
            var items = Sut.ParseLoggable(dummy);

            Assert.Equal(2, items.Count());
            Assert.Equal($"{nameof(DummyPocoShouldLog)}_{nameof(DummyPocoShouldLog.Property1)}", items.ElementAt(0).Key);
            Assert.Equal("Hello", items.ElementAt(0).Value);
            Assert.Equal($"{nameof(DummyPocoShouldLog)}_{nameof(DummyPocoShouldLog.Property2)}", items.ElementAt(1).Key);
            Assert.Equal("World", items.ElementAt(1).Value);
        }

        [Fact]
        public void ParsesSensitive() {
            var dummy = new DummyPocoShouldLogSensitive() {
                Property1 = "Hello",
                Property2 = "World",
                Property3 = "JeffBot"
            };
            var items = Sut.ParseLoggable(dummy);

            Assert.Equal(2, items.Count());
            Assert.Equal($"{nameof(DummyPocoShouldLogSensitive)}_{nameof(DummyPocoShouldLogSensitive.Property1)}", items.ElementAt(0).Key);
            Assert.Equal("Hello", items.ElementAt(0).Value);
            Assert.Equal($"{nameof(DummyPocoShouldLogSensitive)}_{nameof(DummyPocoShouldLogSensitive.Property3)}", items.ElementAt(1).Key);
            Assert.Equal("SENSITIVE", items.ElementAt(1).Value);
        }
    }
}