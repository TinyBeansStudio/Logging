namespace TinyBeans.Logging.Models {

    /// <summary>
    /// A logging template.
    /// </summary>
    public struct Template {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The unique id of the template.</param>
        /// <param name="value">The value of the template.</param>
        public Template(int id, string value) {
            Id = id;
            Value = value;
        }

        /// <summary>
        /// The unique id of the template.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The value of the template.
        /// </summary>
        public string Value { get; }
    }
}