namespace DevKits.Data.Core;
using DevKits.OddsAndEnds.Core.Text;


/// <summary>
/// Represents a fully qualified name for a view in a database schema, providing utility methods for equality comparison
/// and implicit conversion from strings. This class extends <see cref="SchemaObjectNameBase"/> to specifically handle view names.
/// </summary>
public sealed class QualifiedViewName : SchemaObjectNameBase, IEquatable<QualifiedViewName?>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiedViewName"/> class using a fully qualified SQL object name.
    /// </summary>
    /// <param name="qualifiedSqlObjectName">The fully qualified name of the SQL object.</param>
    public QualifiedViewName(string qualifiedSqlObjectName) : base(qualifiedSqlObjectName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiedViewName"/> class by copying an existing instance.
    /// </summary>
    /// <param name="source">The instance of <see cref="QualifiedViewName"/> to copy.</param>
    public QualifiedViewName(QualifiedViewName source) : base(source)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiedViewName"/> class using separate schema and table names.
    /// </summary>
    /// <param name="schemaName">The name of the schema. This parameter is optional.</param>
    /// <param name="objectName">The name of the schema-owned table object.</param>
    public QualifiedViewName(string schemaName, string objectName) : base(schemaName, objectName)
    {
    }

    /// <summary>
    /// Gets the name of the view.
    /// </summary>
    /// <value>The name of the view represented by this instance.</value>
    public string ViewName => MyObjectName;

    /// <summary>
    /// Defines an implicit conversion of a string to a <see cref="QualifiedViewName"/>.
    /// </summary>
    /// <param name="source">The string to convert to a <see cref="QualifiedViewName"/>.</param>
    /// <returns>A <see cref="QualifiedViewName"/> that represents the table name specified by the <paramref name="source"/>.</returns>
    public static implicit operator QualifiedViewName(string source)
    {
        return new QualifiedViewName(source);
    }


    #region Implementation of IEquatable<QualifiedTableName>


    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(QualifiedViewName? other)
    {
        return TSQLRosetta.AreEqual((ISchemaObjectName?)this, other);
    }


    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    ///     <see langword="true" /> if the specified object  is equal to the current object; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return base.Equals(obj);
    }

    /// <summary>Serves as the default hash function.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return CaseInsensitiveHashCode.Combine(SchemaName, MyObjectName);
    }

    /// <summary>
    ///     Returns a value that indicates whether the values of two
    ///     <see cref="QualifiedViewName" /> objects are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    ///     true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise,
    ///     false.
    /// </returns>
    public static bool operator ==(QualifiedViewName? left, QualifiedViewName? right)
    {
        return TSQLRosetta.AreEqual(left, (ISchemaObjectName?)right);
    }

    /// <summary>
    ///     Returns a value that indicates whether two <see cref="QualifiedViewName" /> objects
    ///     have different values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
    public static bool operator !=(QualifiedViewName? left, QualifiedViewName? right)
    {
        return TSQLRosetta.AreNotEqual(left, right);
    }


    #endregion

}