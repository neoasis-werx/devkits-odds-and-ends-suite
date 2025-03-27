namespace DevKits.Data.Core;
using DevKits.OddsAndEnds.Core.Text;

/// <summary>
/// Serves as the base class for representing a database object name with optional schema specification.
/// This class provides foundational functionality for parsing, storing, and accessing the names of schema-owned database objects.
/// </summary>
/// <remarks>
/// This class is used to make Object specific ISchemaObjectNames for database objects. Derived classes are expected to delegate to the protected <see cref="MyObjectName"/> for example
/// <see cref="QualifiedTableName.TableName"/> implementation is <c>public string TableName =&gt; MyObjectName;</c> and
/// <see cref="QualifiedViewName.ViewName"/> implementation is <c>public string ViewName =&gt; MyObjectName;</c>
/// </remarks>
public abstract class SchemaObjectNameBase : ISchemaObjectName
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SchemaObjectNameBase"/> class using a fully qualified SQL object name.
    /// This constructor parses the qualified SQL object name to extract the schema and object names.
    /// </summary>
    /// <param name="qualifiedSqlObjectName">The fully qualified name of the SQL object, including the schema name and object name.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="qualifiedSqlObjectName"/> is null.</exception>
    /// <remarks>
    /// If the schema name is not specified in <paramref name="qualifiedSqlObjectName"/>, the default schema ('dbo') is assumed.
    /// </remarks>
    protected SchemaObjectNameBase(string qualifiedSqlObjectName)
    {
        ArgumentNullException.ThrowIfNull(qualifiedSqlObjectName);

        var parsedName = TSQLRosetta.ParseNameComponents(qualifiedSqlObjectName);
        MyObjectName = parsedName.ObjectName ?? qualifiedSqlObjectName;
        SchemaName = parsedName.SchemaName ?? "dbo";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SchemaObjectNameBase"/> class by copying an existing instance.
    /// </summary>
    /// <param name="source">The instance of <see cref="SchemaObjectNameBase"/> to copy.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
    protected SchemaObjectNameBase(SchemaObjectNameBase source)
    {
        ArgumentNullException.ThrowIfNull(source);
        SchemaName = source.SchemaName;
        MyObjectName = source.MyObjectName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SchemaObjectNameBase"/> class with an optional schema name and a mandatory object name.
    /// </summary>
    /// <param name="schemaName">The name of the schema. If null, the default schema ('dbo') is used.</param>
    /// <param name="objectName">The name of the object owned by the schema.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="objectName"/> is null.</exception>
    protected SchemaObjectNameBase(string? schemaName, string objectName)
    {
        ArgumentNullException.ThrowIfNull(objectName);
        SchemaName = schemaName ?? "dbo";
        MyObjectName = objectName;
    }

    /// <summary>
    /// Gets the name of the schema.
    /// </summary>
    /// <value>The name of the schema. Defaults to 'dbo' if not specified.</value>
    public string SchemaName { get; }

    /// <summary>
    /// Gets the name of the object owned by the schema. Protected access modifier is used to limit modifications to derived classes.
    /// This Method should be delegated to by the derived class in order to make the Object Specific class.
    /// </summary>
    /// <value>The name of the object within the schema.</value>
    protected string MyObjectName { get; }

    /// <summary>
    /// Implements the <see cref="ISchemaObjectName.ObjectName"/> property to expose the name of the object.
    /// </summary>
    /// <value>The name of the object.</value>
    string ISchemaObjectName.ObjectName => MyObjectName;


    #region Formatting

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return TSQLRosetta.JoinIfNotEmpty(".", SchemaName, MyObjectName);
    }

    public string ToQuotedSqlString()
    {
        return TSQLRosetta.JoinIfNotEmpty(".", TSQLRosetta.QuoteName(SchemaName), TSQLRosetta.QuoteName(MyObjectName));
    }

    #endregion

    #region Conversion Operators




    /// <summary>
    ///     Implicit cast that converts the given <see cref="SchemaObjectNameBase" /> to a string.
    /// </summary>
    /// <param name="source">Source for the.</param>
    /// <returns>The result of the operation.</returns>
    public static implicit operator string(SchemaObjectNameBase source)
    {
        return source.ToString();
    }

    #endregion

    #region Equality members

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public virtual bool Equals(ISchemaObjectName? other)
    {
        return TSQLRosetta.AreEqual(this, other);
    }


    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    ///     <see langword="true" /> if the specified object  is equal to the current object; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as SchemaObjectNameBase);
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
    public static bool operator ==(SchemaObjectNameBase? left, SchemaObjectNameBase? right)
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
    public static bool operator !=(SchemaObjectNameBase? left, SchemaObjectNameBase? right)
    {
        return TSQLRosetta.AreNotEqual(left, right);
    }

    #endregion

    #region Comparison

    /// <summary>Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.</summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
    /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
    public int CompareTo(ISchemaObjectName? other)
    {
        return TSQLRosetta.CompareSchemaObjectParts(SchemaName, MyObjectName, other?.SchemaName, other?.ObjectName);
    }

    #region Comparison Operators

    /// <summary>
    /// Less-than comparison operator.
    /// </summary>
    /// <param name="leftSide">The left operand.</param>
    /// <param name="rightSide">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    public static bool operator <(SchemaObjectNameBase leftSide, ISchemaObjectName rightSide)
    {
        return Compare(leftSide, rightSide) < 0;
    }

    /// <summary>
    /// Greater-than comparison operator.
    /// </summary>
    /// <param name="leftSide">The left operand.</param>
    /// <param name="rightSide">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    public static bool operator >(SchemaObjectNameBase leftSide, ISchemaObjectName rightSide)
    {
        return Compare(leftSide, rightSide) > 0;
    }

    /// <summary>
    /// Equality <c>operator</c>.
    /// </summary>
    /// <param name="leftSide">The left operand.</param>
    /// <param name="rightSide">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    public static bool operator ==(SchemaObjectNameBase leftSide, ISchemaObjectName rightSide)
    {
        return Compare(leftSide, rightSide) == 0;
    }

    /// <summary>
    /// Inequality <c>operator</c>.
    /// </summary>
    /// <param name="leftSide">The left operand.</param>
    /// <param name="rightSide">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    public static bool operator !=(SchemaObjectNameBase leftSide, ISchemaObjectName rightSide)
    {
        return Compare(leftSide, rightSide) != 0;
    }

    /// <summary>
    /// Less-than-or-equal comparison operator.
    /// </summary>
    /// <param name="leftSide">The left operand.</param>
    /// <param name="rightSide">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    public static bool operator <=(SchemaObjectNameBase leftSide, ISchemaObjectName rightSide)
    {
        return Compare(leftSide, rightSide) <= 0;
    }

    /// <summary>
    /// Greater-than-or-equal comparison operator.
    /// </summary>
    /// <param name="leftSide">The left operand.</param>
    /// <param name="rightSide">The right operand.</param>
    /// <returns>The result of the operation.</returns>
    public static bool operator >=(SchemaObjectNameBase leftSide, ISchemaObjectName rightSide)
    {
        return Compare(leftSide, rightSide) >= 0;
    }

    /// <summary>
    /// Compares two OrderNumber objects to determine their relative ordering.
    /// </summary>
    /// <param name="leftSide">Order number to be compared.</param>
    /// <param name="rightSide">Order number to be compared.</param>
    /// <returns>
    /// Negative if 'leftSide' is less than 'rightSide', 0 if they are equal, or positive if it is greater.
    /// </returns>
    public static int Compare(ISchemaObjectName leftSide, ISchemaObjectName rightSide)
    {
        return TSQLRosetta.Compare(leftSide, rightSide);
    }

    #endregion

    #endregion

}