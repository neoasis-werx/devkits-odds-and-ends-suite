namespace DevKits.Data.Core;
using DevKits.OddsAndEnds.Core.Text;

/// <summary>
/// Represents a fully qualified name for a database table column, encapsulating the schema, table, and column names.
/// This class provides functionality to manage and manipulate qualified column names, including support for quoting and comparisons.
/// This class cannot be inherited.
/// </summary>
/// <remarks>
/// This class is useful in scenarios where database operations require precise identification of columns, especially in environments
/// with multiple schemas or where column names may conflict without full qualification.
/// </remarks>
/// <seealso cref="IEquatable{QualifiedTableColumnName}"/>
public sealed class QualifiedTableColumnName : IEquatable<QualifiedTableColumnName>
{
    /// <summary>
    /// Gets the qualified name of the table to which the column belongs.
    /// </summary>
    /// <value>The qualified name of the table.</value>
    public QualifiedTableName QualifiedTableName { get; }

    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    /// <value>The name of the column.</value>
    public string ColumnName { get; }

    /// <summary>
    /// Gets the quoted name of the column, ensuring it is formatted correctly for use in SQL queries.
    /// </summary>
    /// <value>The quoted name of the column.</value>
    public string QuotedColumnName => TSQLRosetta.QuoteName(ColumnName)!;

    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiedTableColumnName"/> class with a specified qualified table name and column name.
    /// </summary>
    /// <param name="qualifiedTableName">The fully qualified name of the table.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <exception cref="ArgumentNullException">Thrown if either <paramref name="qualifiedTableName"/> or <paramref name="columnName"/> is null.</exception>
    public QualifiedTableColumnName(QualifiedTableName qualifiedTableName, string columnName)
    {
        QualifiedTableName = qualifiedTableName ?? throw new ArgumentNullException(nameof(qualifiedTableName));
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiedTableColumnName"/> class with specified schema name, table name, and column name.
    /// </summary>
    /// <param name="schemaName">The name of the schema.</param>
    /// <param name="tableName">The name of the table.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="schemaName"/>, <paramref name="tableName"/>, or <paramref name="columnName"/> is null.</exception>
    public QualifiedTableColumnName(string schemaName, string tableName, string columnName)
    {
        ArgumentNullException.ThrowIfNull(schemaName);
        ArgumentNullException.ThrowIfNull(tableName);
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));

        QualifiedTableName = new QualifiedTableName(schemaName, tableName);
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="QualifiedTableColumnName"/> object.
    /// </summary>
    /// <returns>A string that represents the current object, formatted as 'SchemaName.TableName.ColumnName'.</returns>
    public override string ToString()
    {
        return TSQLRosetta.JoinIfNotEmpty(".", QualifiedTableName, ColumnName);
    }

    /// <summary>
    /// Converts this <see cref="QualifiedTableColumnName"/> to a quoted SQL string, suitable for use in SQL queries.
    /// </summary>
    /// <returns>A quoted string representing this fully qualified column name.</returns>
    public string ToQuotedSqlString()
    {
        return TSQLRosetta.JoinIfNotEmpty(".", QualifiedTableName.ToQuotedSqlString(), QuotedColumnName);
    }


    #region Equality members

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(QualifiedTableColumnName? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return QualifiedTableName.Equals(other.QualifiedTableName) && TSQLRosetta.AreEqual(ColumnName, other.ColumnName);
    }

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((QualifiedTableColumnName)obj);
    }

    /// <summary>Serves as the default hash function.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return CaseInsensitiveHashCode.Combine(QualifiedTableName, ColumnName);
    }

    /// <summary>Returns a value that indicates whether the values of two <see cref="QualifiedTableColumnName" /> objects are equal.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
    public static bool operator ==(QualifiedTableColumnName? left, QualifiedTableColumnName? right)
    {
        return AreEqual(left, right);
    }


    /// <summary>Returns a value that indicates whether two <see cref="QualifiedTableColumnName" /> objects have different values.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
    public static bool operator !=(QualifiedTableColumnName? left, QualifiedTableColumnName? right)
    {
        return !AreEqual(left, right);
    }

    private static bool AreEqual(QualifiedTableColumnName? left, QualifiedTableColumnName? right)
    {
        return TSQLRosetta.AreEqual(left?.QualifiedTableName, (ISchemaObjectName?)right?.QualifiedTableName) && TSQLRosetta.AreEqual(left?.ColumnName, right?.ColumnName);
    }

    #endregion
}
