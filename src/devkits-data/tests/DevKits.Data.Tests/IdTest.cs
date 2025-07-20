namespace DevKits.Data.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>
    ///     Represents a test class for identifiers, implementing IParsable and IFormattable interfaces.
    /// </summary>
    public class IdTest : IParsable<IdTest>, IFormattable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IdTest" /> class.
        /// </summary>
        /// <param name="identifier">The identifier value.</param>
        /// <param name="identifierName">The name of the identifier.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="identifierName" /> is null.</exception>
        public IdTest(int identifier, string identifierName)
        {
            Identifier = identifier;
            IdentifierName = identifierName ?? throw new ArgumentNullException(nameof(identifierName));
        }

        /// <summary>
        ///     Gets or sets the identifier value.
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        ///     Gets or sets the name of the identifier.
        /// </summary>
        public string IdentifierName { get; set; }

        #region Implementation of IFormattable

        /// <summary>
        ///     Converts the value of the current instance to its equivalent string representation using the specified format and
        ///     format provider.
        /// </summary>
        /// <param name="format">The format to use.</param>
        /// <param name="formatProvider">The format provider to use.</param>
        /// <returns>The string representation of the current instance.</returns>
        /// <exception cref="FormatException">Thrown when the format string is not supported.</exception>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            switch (format.ToUpperInvariant())
            {
                case "G":
                    return $"{IdentifierName.ToUpperInvariant()}|{Identifier}";
                case "I":
                    return Identifier.ToString(formatProvider);
                case "N":
                    return IdentifierName;
                default:
                    throw new FormatException($"The '{format}' format string is not supported.");
            }
        }

        #endregion

        #region Implementation of IParsable<IdTest>

        /// <summary>
        ///     Parses a string to create an <see cref="IdTest" /> instance.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="provider">The format provider.</param>
        /// <returns>An <see cref="IdTest" /> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s" /> is null.</exception>
        /// <exception cref="FormatException">Thrown when the input string is not in the correct format.</exception>
        public static IdTest Parse(string s, IFormatProvider? provider)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            var parts = s.Split('|');
            if (parts.Length != 2)
            {
                throw new FormatException($"Input string '{s}' is not in the correct format.");
            }

            if (!int.TryParse(parts[1], NumberStyles.Integer, provider, out var identifier))
            {
                throw new FormatException($"Identifier part '{parts[1]}' is not in the correct format.");
            }

            return new IdTest(identifier, parts[0]);
        }

        /// <summary>
        ///     Tries to parse a string to create an <see cref="IdTest" /> instance.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="provider">The format provider.</param>
        /// <param name="result">The resulting <see cref="IdTest" /> instance if parsing is successful.</param>
        /// <returns><c>true</c> if parsing is successful; otherwise, <c>false</c>.</returns>
        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out IdTest result)
        {
            if (s == null)
            {
                result = null!;
                return false;
            }

            try
            {
                result = Parse(s, provider);
                return true;
            }
            // ReSharper disable once CatchAllClause
            catch
            {
                result = null!;
                return false;
            }
        }

        #endregion
    }
}