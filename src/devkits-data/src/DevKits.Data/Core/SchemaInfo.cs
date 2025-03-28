
using DevKits.Data.Abstractions;

namespace DevKits.Data.Core;

/// <summary>
/// Represents information about a database schema.
/// </summary>
public class SchemaInfo : ISchemaInfo
{
    /// <summary>
    /// Gets or sets the name of the schema.
    /// </summary>
    public string SchemaName { get; set; }

    /// <summary>
    /// Gets or sets the list of tables in the schema.
    /// </summary>
    public TableCollection Tables { get; set; } = new TableCollection();

    /// <summary>
    /// Initializes a new instance of the <see cref="SchemaInfo"/> class.
    /// </summary>
    /// <param name="schemaName">The name of the schema.</param>
    public SchemaInfo(string schemaName)
    {
        SchemaName = schemaName ?? throw new ArgumentNullException(nameof(schemaName));
    }
}
