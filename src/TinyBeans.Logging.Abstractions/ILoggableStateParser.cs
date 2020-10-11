using System.Collections.Generic;

namespace TinyBeans.Logging.Abstractions {

    /// <summary>
    /// Represents a type used to supply loggable items for an object.
    /// </summary>
    public interface ILoggableStateParser {

        /// <summary>
        /// Used to parse the loggable items from a state object.
        /// </summary>
        /// <typeparam name="TState">The type of object to parse the loggable items from.</typeparam>
        /// <param name="state">The object to parse the loggable items from.</param>
        /// <returns>A dictionary containing the parsed loggable items.</returns>
        Dictionary<string, object> ParseLoggableItems<TState>(TState state);
    }
}