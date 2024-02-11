namespace DevKits.Data.Core;
using DevKits.OddsAndEnds.Core.Text;

/// <summary>
/// A qualified table column name. This class cannot be inherited.
/// </summary>
///
/// <seealso cref="IEquatable{QualifiedTableColumnName}"/>
public sealed class QualifiedTableColumnName : IEquatable<QualifiedTableColumnName>
{

    public QualifiedTableName QualifiedTableName { get; }
    public string ColumnName { get; }

    /// <summary>
    /// Gets the the quoted column.
    /// </summary>
    ///
    /// <value>The name of the quoted column.</value>
    public string QuotedColumnName => TSQLRosetta.QuoteName(ColumnName)!;

    /// <summary>
    /// Initializes a new instance of the <see cref="object" /> class.
    /// </summary>
    ///
    /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
    ///
    /// <param name="qualifiedTableName">Name of the table.</param>
    /// <param name="columnName">Name of the column.</param>
    public QualifiedTableColumnName(QualifiedTableName qualifiedTableName, string columnName)
    {
        QualifiedTableName = qualifiedTableName ?? throw new ArgumentNullException(nameof(qualifiedTableName));
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
    }

    /// <summary>
    /// Initializes a new instance of the DevKits.Data.Core.QualifiedTableColumnName class.
    /// </summary>
    ///
    /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
    ///
    /// <param name="schemaName">Name of the schema.</param>
    /// <param name="tableName">Name of the table.</param>
    /// <param name="columnName">Name of the column.</param>
    public QualifiedTableColumnName(string schemaName, string tableName, string columnName)
    {
        ArgumentNullException.ThrowIfNull(schemaName);
        ArgumentNullException.ThrowIfNull(tableName);

        QualifiedTableName = new QualifiedTableName(schemaName, tableName);
        ColumnName = columnName ?? throw new ArgumentNullException(nameof(columnName));
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return TSQLRosetta.JoinIfNotEmpty(".", QualifiedTableName, ColumnName) ?? string.Empty;
    }

    /// <summary>
    /// Converts this QualifiedTableColumnName to a quoted SQL string.
    /// </summary>
    ///
    /// <returns>This QualifiedTableColumnName as a string.</returns>
    public string ToQuotedSqlString()
    {
        return TSQLRosetta.JoinIfNotEmpty(".", QualifiedTableName.ToQuotedSqlString(), TSQLRosetta.QuoteName(ColumnName)) ?? string.Empty;
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
