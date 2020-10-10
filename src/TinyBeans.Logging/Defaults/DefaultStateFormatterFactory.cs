using System;

namespace TinyBeans.Logging.Defaults {

    /// <summary>
    /// The default implementation of <see cref="IStateFormatterFactory"/>.
    /// </summary>
    public class DefaultStateFormatterFactory : IStateFormatterFactory {

        /// <summary>
        /// Creates a formatter for the specified state.
        /// </summary>
        /// <typeparam name="TState">The type of object to format.</typeparam>
        /// <param name="state">The object to format.</param>
        /// <returns>The requested formatter.</returns>
        public Func<TState, Exception, string> GetFormatter<TState>(TState state) {
            throw new NotImplementedException();
        }

        private static Func<TState, Exception, string> Formatter<TState>(TState state) => (TState state, Exception exception) => {
            return string.Empty;
        };
    }
}