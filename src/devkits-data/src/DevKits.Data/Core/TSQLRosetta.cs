#pragma warning disable S101
#pragma warning disable IDE0251 // member can be made Readonly
// ReSharper disable once InconsistentNaming
//
namespace DevKits.Data.Core
{
    using System;
    using System.Linq;

    /// <summary>
    /// A TSQL language rosetta Class. Dotnet Versions of TSQL Functions.
    /// </summary>
    public static class TSQLRosetta
    {
        /// <summary>
        /// Returns the specified part of an object name. The parts of an object that can be retrieved are the object name,
        /// schema name, database name, and server name. This is functionally equivalent to T-SQL
        /// <code>PARSENAME('object_name' , object_piece )</code>
        /// function.
        /// </summary>
        /// <remarks>
        /// 'objectName' Is the parameter that holds the name of the object for which to retrieve the specified object part.
        /// This parameter is an optionally-qualified object name. If all parts of the object name are qualified, this name
        /// can have four parts: the server name, the database name, the schema name, and the object name.
        /// </remarks>
        /// <param name="objectName">
        /// 'objectName' Is the parameter that holds the name of the object for which to retrieve the specified object part.
        /// This parameter is an optionally-qualified object name. If all parts of the object name are qualified, this name
        /// can have four parts: the server name, the database name, the schema name, and the object name.
        /// </param>
        /// <param name="objectPiece">Is the object part to return. objectPiece is of type int, and can have these values:
        ///        1 = Object name
        ///        2 = Schema name
        ///        3 = Database name
        ///        4 = Server name
        /// </param>
        /// <returns>A string.</returns>
        public static string? ParseName(string objectName, int objectPiece)
        {
            if (objectName == null) throw new ArgumentNullException(nameof(objectName));
            var parts = objectName.Split('.').Reverse().ToArray();
            if (0 < objectPiece && objectPiece <= parts.Length) return parts[objectPiece - 1];
            return null;
        }

        /// <summary>
        /// Parse name into components.
        /// </summary>
        ///
        /// <param name="qualifiedSqlObjectName">Name of the qualified SQL object.</param>
        /// <remarks>Functionally equivalent to <see cref="ParseName"/> but returns an object.</remarks>
        /// <returns>A DbSchemaObjectName.</returns>
        public static DbSchemaObjectName ParseNameComponents(string qualifiedSqlObjectName)
        {
            // reverse array Loop and switch cleanly handles variable sized arrays.
            var parts = qualifiedSqlObjectName.Split('.').Reverse().ToArray();

            var p = new DbSchemaObjectName();

            for (var i = 0; i < parts.Length; i++)
                switch (i)
                {
#pragma warning disable S2583
                    case 0: p.ObjectName = RemoveQuotes(parts[i]); break;
                    case 1: p.SchemaName = RemoveQuotes(parts[i]); break;
                    case 2: p.DatabaseName = RemoveQuotes(parts[i]); break;
                    case 3: p.ServerName = RemoveQuotes(parts[i]); break;
#pragma warning restore S2583
                }

            p.ObjectName ??= qualifiedSqlObjectName;
            p.SchemaName ??= "dbo";
            return p;
        }

        /// <summary>
        /// A Database Schema Object Name. Represents the Possible parts of a Database Name as parsed from <see href="https://learn.microsoft.com/en-us/sql/t-sql/functions/parsename-transact-sql?view=sql-server-ver16">
        /// PARSENAME()</see>
        /// </summary>
        public struct DbSchemaObjectName
        {
            /// <summary>
            /// Initializes a new instance of the DbSchemaObjectName class.
            /// </summary>
            public DbSchemaObjectName()
            {
                DatabaseName = null;
                ServerName = null;
            }

            /// <summary>
            /// Gets or sets the name of the object.
            /// </summary>
            ///
            /// <value>The name of the object.</value>
            public string? ObjectName { get; set; } = null!;
            public string? SchemaName { get; set; } = null!;
            public string? DatabaseName { get; set; }
            public string? ServerName { get; set; }

            /// <summary>
            /// Gets a value indicating whether this DbSchemaObjectName is server qualified.
            /// </summary>
            ///
            /// <value>True if this TSQLRosetta is server qualified, false if not.</value>
            public bool IsServerQualified => ServerName != null;

            /// <summary>
            /// Gets a value indicating whether this DbSchemaObjectName is database qualified.
            /// </summary>
            ///
            /// <value>True if this TSQLRosetta is database qualified, false if not.</value>
            public bool IsDatabaseQualified => DatabaseName != null;

            /// <summary>
            /// Indexer to get NameParts within this collection using array index syntax.
            /// </summary>
            ///
            /// <param name="index">Zero-based index of the entry to access.</param>
            ///
            /// <returns>The indexed item.</returns>
            public string? this[int index]
            {
                get
                {
                    return index switch
                    {
                        0 => ObjectName,
                        1 => SchemaName,
                        2 => DatabaseName,
                        3 => ServerName,
                        _ => null
                    };
                }
            }

            public override string ToString()
            {
                return JoinIfNotEmpty(".", ServerName, DatabaseName, SchemaName, ObjectName) ?? string.Empty;
            }

            public string ToQuotedSqlString()
            {
                return JoinIfNotEmpty(".", QuoteName(ServerName), QuoteName(DatabaseName), QuoteName(SchemaName), QuoteName(ObjectName)) ?? string.Empty;
            }

        }

        /// <summary>
        /// Returns a Unicode string with the delimiters added to make the input string a valid SQL Server delimited
        /// identifier. This is functionally equivalent to T-SQL <code>QUOTENAME ( 'character_string' [ , 'quote_character' ]
        /// )</code>
        /// </summary>
        /// <param name="characterString">
        /// Is a string of Unicode character data. character_string is <code>sysname</code> and is limited to 128 characters. Inputs
        /// greater than 128 characters return NULL.
        /// </param>
        /// <param name="quoteCharacters">
        /// Is a one-character string to use as the delimiter. Can be a single quotation mark ( ' ), a left or right bracket
        /// ( [] ), a double quotation mark ( " ), a left or right parenthesis ( () ), a greater than or less than sign ( &gt;
        /// &lt; ), a left or right brace ( {} ) or a backtick ( ` ). NULL returns if an unacceptable character is supplied.
        /// If quote_character is not specified, brackets are used.
        /// </param>
        /// <returns>A string.</returns>
        public static string? QuoteName(string? characterString, string quoteCharacters = "[]")
        {
            if (string.IsNullOrWhiteSpace(characterString)) return null;

            char prefix;
            char suffix;

            switch (quoteCharacters)
            {
                case null:
                case "[]": prefix = '['; suffix = ']'; break;
                case "'": prefix = suffix = '\''; break;
                case "\"": prefix = suffix = '\"'; break;
                case "()": prefix = '('; suffix = ')'; break;
                case "><": prefix = '>'; suffix = '<'; break;
                case "{}": prefix = '{'; suffix = '}'; break;
                case "`": prefix = suffix = '`'; break;
                default: return null;
            }

            return $"{prefix}{characterString.Trim(prefix, suffix)}{suffix}";
        }

        /// <summary>
        /// Removes the quotes described found in characterString. This is the opposite of <see cref="QuoteName"/>.
        /// </summary>
        ///
        /// <param name="characterString">
        /// Is a string of Unicode character data. character_string is <code>sysname</code> and is limited to 128 characters.
        /// Inputs greater than 128 characters return NULL.
        /// </param>
        ///
        /// <returns>A string.</returns>
        public static string? RemoveQuotes(string? characterString)
        {
            return characterString?.Trim('[', ']', '\'', '"', '(', ')', '<', '>', '{', '}', '`');
        }

        /// <summary>
        /// Determines whether two strings are equal, ignoring case.
        /// </summary>
        /// <param name="source">The first string to compare, or <c>null</c>.</param>
        /// <param name="other">The second string to compare, or <c>null</c>.</param>
        /// <returns><c>true</c> if the strings are equal; otherwise, <c>false</c>.</returns>
        public static bool AreEqual(string? source, string? other)
        {
            return string.Equals(source, other, StringComparison.OrdinalIgnoreCase);
        }

        public static bool AreEqual(ISchemaObjectName? first, ISchemaObjectName? other)
        {
            if (ReferenceEquals(first, other)) return true;
            if (first is null) return false;
            if (other is null) return false;
            return AreEqual(first.SchemaName, other.SchemaName) && AreEqual(first.ObjectName, other.ObjectName);
        }

        public static bool AreNotEqual(ISchemaObjectName? first, ISchemaObjectName? other)
        {
            return !AreEqual(first, other);
        }

        /// <summary>
        /// Gets the hash code for a string, ignoring case.
        /// </summary>
        /// <param name="sqlName">The string for which to get the hash code.</param>
        /// <returns>The hash code of the string.</returns>
        public static int GetHashCode(string sqlName)
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(sqlName);
        }

        /// <summary>
        /// Compares two strings, ignoring case.
        /// </summary>
        /// <param name="source">The first string to compare, or <c>null</c>.</param>
        /// <param name="other">The second string to compare, or <c>null</c>.</param>
        /// <returns>
        /// A positive integer if <paramref name="source"/> is greater than <paramref name="other"/>,
        /// zero if they are equal, and a negative integer if <paramref name="source"/> is less than <paramref name="other"/>.
        /// </returns>
        public static int Compare(string? source, string? other)
        {
            return string.Compare(source, other, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Compares two <see cref="ISchemaObjectName"/> instances, ignoring case.
        /// </summary>
        /// <param name="source">The first <see cref="ISchemaObjectName"/> to compare, or <c>null</c>.</param>
        /// <param name="other">The second <see cref="ISchemaObjectName"/> to compare, or <c>null</c>.</param>
        /// <returns>
        /// A positive integer if <paramref name="source"/> is greater than <paramref name="other"/>,
        /// zero if they are equal, and a negative integer if <paramref name="source"/> is less than <paramref name="other"/>.
        /// </returns>
        public static int Compare(ISchemaObjectName? source, ISchemaObjectName? other)
        {
            return CompareSchemaObjectParts(source?.SchemaName, source?.ObjectName, other?.SchemaName, other?.ObjectName);
        }

        /// <summary>
        /// Compares the schema and object name parts of two schema objects, ignoring case.
        /// </summary>
        /// <param name="firstSchemaName">The schema name of the first schema object.</param>
        /// <param name="firstTableName">The object name of the first schema object.</param>
        /// <param name="otherSchemaName">The schema name of the second schema object.</param>
        /// <param name="otherTableName">The object name of the second schema object.</param>
        /// <returns>
        /// A positive integer if the first schema object is greater than the second,
        /// zero if they are equal, and a negative integer if the first schema object is less than the second.
        /// </returns>
        public static int CompareSchemaObjectParts(string? firstSchemaName, string? firstTableName, string? otherSchemaName, string? otherTableName)
        {
            var schemaCompare = Compare(firstSchemaName, otherSchemaName);
            return schemaCompare != 0 ? schemaCompare : Compare(firstTableName, otherTableName);
        }




        /// <summary>
        /// Concatenates the elements of a string array, using the specified delimiter between each element,
        /// excluding empty or null strings.
        /// </summary>
        /// <param name="delimiter">The string to use as a delimiter. Delimiter is included in the returned string only if values exist to be joined.</param>
        /// <param name="stringArgs">An array of strings to concatenate. Null or empty strings in the array are ignored.</param>
        /// <returns>
        /// A string that consists of the non-empty elements in <paramref name="stringArgs"/> delimited by the <paramref name="delimiter"/> string.
        /// Returns <see langword="null"/> if <paramref name="stringArgs"/> is null or all elements are empty or null.
        /// Returns an empty string if <paramref name="stringArgs"/> contains no elements.
        /// </returns>
        /// <remarks>
        /// This method is useful for creating a concatenated string from an array of values while ignoring any null or empty values.
        /// It provides a clean way to join strings with a delimiter without needing to filter the array beforehand.
        /// </remarks>
        public static string? JoinIfNotEmpty(string delimiter, params string?[] stringArgs)
        {
            return string.Join(delimiter, stringArgs.Where(s => !string.IsNullOrEmpty(s)));
        }


        /// <summary>
        /// Compares two lists of strings to determine if they are equal.
        /// </summary>
        /// <param name="source">The first list of strings to compare.</param>
        /// <param name="other">The second list of strings to compare.</param>
        /// <returns>
        /// <c>true</c> if both lists contain the same strings in the same order; otherwise, <c>false</c>.
        /// Returns <c>true</c> if both lists are null.
        /// </returns>
        /// <remarks>
        /// This method performs an ordinal comparison of each string in the lists.
        /// Null lists are considered equal, but a null list is not equal to a non-null list.
        /// The comparison is case-insensitive and depends on the implementation of <see cref="TSQLRosetta.AreEqual(string,string)"/>
        /// for comparing individual string elements.
        /// </remarks>
        public static bool AreEquals(IList<string>? source, IList<string>? other)
        {
            // Check if both source and other are null, which are considered equal
            if (source == null && other == null) return true;

            // If only one is null, they are not equal
            if (source == null || other == null) return false;

            // Lists with different counts cannot be equal
            if (source.Count != other.Count) return false;

            // Iterate through the lists comparing each corresponding element
            for (var i = 0; i < source.Count; i++)
            {
                // Use TSQLRosetta.AreEqual to compare the elements
                // If any pair of elements is not equal, the lists are not equal
                if (!TSQLRosetta.AreEqual(source[i], other[i]))
                {
                    return false;
                }
            }

            // If all elements are equal, return true
            return true;
        }



    }
}


