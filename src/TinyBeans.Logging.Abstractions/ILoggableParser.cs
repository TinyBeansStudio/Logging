using System.Collections.Generic;

namespace TinyBeans.Logging {

    /// <summary>
    /// Represents a type used to parse loggable items from a state object.
    /// </summary>
    public interface ILoggableParser {

        /// <summary>
        /// Used to parse loggable items from a state object.
        /// </summary>
        /// <typeparam name="TState">The type of object to parse the loggable items.</typeparam>
        /// <param name="state">The object to parse the loggable items.</param>
        /// <returns>A dictionary containing the parsed loggable items.</returns>
        IEnumerable<KeyValuePair<string, object>> ParseLoggable<TState>(TState state);
    }
}