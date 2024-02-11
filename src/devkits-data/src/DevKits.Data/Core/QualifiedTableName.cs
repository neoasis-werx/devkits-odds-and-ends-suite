namespace DevKits.Data.Core;
using DevKits.OddsAndEnds.Core.Text;
using OddsAndEnds.Core;

public sealed class QualifiedTableName : SchemaObjectNameBase, IEquatable<QualifiedTableName>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SchemaObjectNameBase" /> class.
    /// </summary>
    /// <param name="qualifiedSqlObjectName">Name of the qualified SQL object.</param>
    public QualifiedTableName(string qualifiedSqlObjectName) : base(qualifiedSqlObjectName)
    {
    }

    /// <summary>
    ///     Copy Constructor for <see cref="SchemaObjectNameBase" /> class.
    /// </summary>
    /// <param name="source">Source for the.</param>
    public QualifiedTableName(QualifiedTableName source) : base(source)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SchemaObjectNameBase" /> class.
    /// </summary>
    /// <param name="schemaName">Name of the schema [optional]</param>
    /// <param name="myObjectName">Name of the Schema owned Object.</param>
    public QualifiedTableName(string schemaName, string myObjectName) : base(schemaName, myObjectName)
    {
    }

    /// <summary>
    /// Gets the name of the table.
    /// </summary>
    ///
    /// <value>The name of the table.</value>
    public string TableName => MyObjectName;

    /// <summary>
    /// Implicit cast that converts the given string to a QualifiedTableName.
    /// </summary>
    ///
    /// <param name="source">Source for the.</param>
    ///
    /// <returns>The result of the operation.</returns>
    public static implicit operator QualifiedTableName(string source)
    {
        return new QualifiedTableName(source);
    }

    #region Implementation of IEquatable<QualifiedTableName>

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(QualifiedTableName? other)
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
    ///     <see cref="T:DevKits.CodeGen.QualifiedSQLObjectName" /> objects are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    ///     true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise,
    ///     false.
    /// </returns>
    public static bool operator ==(QualifiedTableName left, QualifiedTableName right)
    {
        return TSQLRosetta.AreEqual(left, (ISchemaObjectName?)right);
    }

    /// <summary>
    ///     Returns a value that indicates whether two <see cref="T:DevKits.CodeGen.QualifiedSQLObjectName" /> objects
    ///     have different values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
    public static bool operator !=(QualifiedTableName left, QualifiedTableName right)
    {
        return TSQLRosetta.AreNotEqual(left, right);
    }

    #endregion
}