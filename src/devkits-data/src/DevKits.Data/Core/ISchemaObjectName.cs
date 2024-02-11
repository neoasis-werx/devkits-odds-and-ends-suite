namespace DevKits.Data.Core;

/// <summary>
/// Defines an interface for representing a schema object's name, offering functionality for comparison and equality checks,
/// as well as formatting the name for SQL queries. This interface is intended for use with database objects that are
/// identified by a schema and object name.
/// </summary>
public interface ISchemaObjectName : IEquatable<ISchemaObjectName>, IComparable<ISchemaObjectName>
{
    /// <summary>
    /// Gets the name of the schema to which the object belongs.
    /// </summary>
    /// <value>The name of the schema as a <see cref="string"/>.</value>
    /// <remarks>
    /// The schema name is typically used in SQL database operations to qualify object names and ensure that
    /// the correct object is referenced in a query, especially when multiple schemas contain objects with the same name.
    /// </remarks>
    string SchemaName { get; }

    /// <summary>
    /// Gets the name of the database object (e.g., table, view, stored procedure).
    /// </summary>
    /// <value>The name of the object as a <see cref="string"/>.</value>
    /// <remarks>
    /// The object name, together with the schema name, uniquely identifies a database object within a database.
    /// </remarks>
    string ObjectName { get; }

    /// <summary>
    /// Converts this instance of <see cref="ISchemaObjectName"/> to a properly quoted SQL string that can be used directly in SQL queries.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the fully qualified name of the schema object, formatted for use in SQL queries.</returns>
    /// <remarks>
    /// This method is intended to produce a string suitable for inclusion in SQL commands, where the schema and object names
    /// are quoted to preserve case sensitivity and to allow for special characters in the names.
    /// </remarks>
    string ToQuotedSqlString();
}
