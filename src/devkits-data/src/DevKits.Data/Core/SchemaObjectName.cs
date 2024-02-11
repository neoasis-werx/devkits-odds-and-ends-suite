namespace DevKits.Data.Core;

public sealed class SchemaObjectName : SchemaObjectNameBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SchemaObjectNameBase" /> class.
    /// </summary>
    /// <param name="qualifiedSqlObjectName">Name of the qualified SQL object.</param>
    public SchemaObjectName(string qualifiedSqlObjectName) : base(qualifiedSqlObjectName)
    {
    }

    /// <summary>
    ///     Copy Constructor for <see cref="SchemaObjectNameBase" /> class.
    /// </summary>
    /// <param name="source">Source for the.</param>
    public SchemaObjectName(SchemaObjectNameBase source) : base(source)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SchemaObjectNameBase" /> class.
    /// </summary>
    /// <param name="schemaName">Name of the schema [optional]</param>
    /// <param name="myObjectName">Name of the Schema owned Object.</param>
    public SchemaObjectName(string schemaName, string myObjectName) : base(schemaName, myObjectName)
    {
    }

    public string ObjectName => MyObjectName;
}