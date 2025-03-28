namespace DevKits.Data.SqlServer.InformationSchema;

using Core;

/// <summary>
/// Represents a table in the information schema.
/// </summary>
public class InfoSchemaTable
{
    /// <summary>
    /// Gets or sets the table catalog.
    /// </summary>
    public string TableCatalog { get; set; } = null!;

    /// <summary>
    /// Gets or sets the table schema.
    /// </summary>
    public string TableSchema { get; set; } = null!;

    /// <summary>
    /// Gets or sets the table name.
    /// </summary>
    public string TableName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the table type.
    /// </summary>
    public string TableType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the table temporal type.
    /// </summary>
    public TableTemporalType TableTemporalType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the table is updatable.
    /// </summary>
    public bool IsUpdatable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the table is a user table.
    /// </summary>
    public bool IsUserTable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the table is a temporal table type.
    /// </summary>
    public bool IsTemporalTableType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the table is a history table for a system versioned table.
    /// </summary>
    public bool IsHistoryTableForSystemVersionedTable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the table is a system versioned temporal table.
    /// </summary>
    public bool IsSystemVersionedTemporalTable { get; set; }

    /// <summary>
    /// Gets or sets the qualified table name.
    /// </summary>
    public QualifiedTableName QualifiedTableName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of columns in the table.
    /// </summary>
    public InfoSchemaColumnCollection Columns { get; set; } = new();
}