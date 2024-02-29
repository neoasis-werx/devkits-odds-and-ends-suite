#pragma warning disable S101

namespace DevKits.Data
{

    using System.Data;
    using OddsAndEnds.Core.Types;

    /// <summary>
    /// Represents an SQL query as a strongly-typed value object. This class encapsulates an SQL query string
    /// along with its command type, providing a semantic way to work with SQL queries within the application.
    /// </summary>
    /// <seealso cref="CommandType"/>
    /// <remarks>
    /// The class supports implicit conversion to and from <see cref="string"/>, allowing for easier integration
    /// with APIs expecting string representations of SQL queries.
    /// </remarks>
    public class SQLQuery : SemanticType<SQLQuery, string>
    {
        /// <summary>
        /// Gets or sets the type of the command associated with the SQL query.
        /// </summary>
        /// <value>The command type, indicating how the command string is interpreted by the data source.</value>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLQuery"/> class with the specified SQL query string.
        /// The command type is set to <see cref="CommandType.Text"/> by default.
        /// </summary>
        /// <param name="value">The SQL query string.</param>
        public SQLQuery(string value) : base(value)
        {
            CommandType = CommandType.Text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLQuery"/> class with the specified SQL query string and command type.
        /// </summary>
        /// <param name="value">The SQL query string.</param>
        /// <param name="commandType">The type of the command, which specifies how the command string is interpreted by the data source.</param>
        protected SQLQuery(string value, CommandType commandType) : base(value)
        {
            CommandType = commandType;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents the current SQL query.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the SQL query.</returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Defines an implicit conversion of an <see cref="SQLQuery"/> instance to a <see cref="string"/>.
        /// </summary>
        /// <param name="source">The <see cref="SQLQuery"/> instance to convert.</param>
        /// <returns>A <see cref="string"/> representation of the SQL query.</returns>
        public static implicit operator SQLQuery(string source)
        {
            return new SQLQuery(source);
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="string"/> to an <see cref="SQLQuery"/> instance.
        /// </summary>
        /// <param name="source">The SQL query <see cref="string"/> to convert.</param>
        /// <returns>An <see cref="SQLQuery"/> instance representing the SQL query.</returns>
        public static implicit operator string(SQLQuery source)
        {
            return source.ToString();
        }
    }
}

