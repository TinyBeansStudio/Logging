using System;

namespace TinyBeans.Logging {

    /// <summary>
    /// Represents a type used to generate logging formatters.
    /// </summary>
    public interface IStateFormatterFactory {

        /// <summary>
        /// Creates a formatter for the specified state.
        /// </summary>
        /// <typeparam name="TState">The type of object to format.</typeparam>
        /// <param name="state">The object to format.</param>
        /// <returns>The requested formatter.</returns>
        Func<TState, Exception, string> GetFormatter<TState>(TState state);
    }
}