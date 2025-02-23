namespace DevKits.Data.Abstractions;

using Core;

public interface ITableInfo
{
    /// <summary>
    /// Gets or sets the qualified table name.
    /// </summary>
    QualifiedTableName TableName { get; set; }

    /// <summary>
    /// Gets the attributes of the table.
    /// </summary>
    TableAttributes Attributes { get; }

    /// <summary>
    /// Gets a value indicating whether the table is a history table for a system-versioned table.
    /// </summary>
    bool IsHistoryTableForSystemVersionedTable { get; }

    /// <summary>
    /// Gets a value indicating whether the table is a system-versioned temporal table.
    /// </summary>
    bool IsSystemVersionedTemporalTable { get; }

    /// <summary>
    /// Gets a value indicating whether the table is a user table.
    /// </summary>
    bool IsUserTable { get; }

    /// <summary>
    /// Gets a value indicating whether the table is a user table type.
    /// </summary>
    bool IsUserTableType { get; }

    /// <summary>
    /// Gets a value indicating whether the table has auto-numbered columns.
    /// </summary>
    bool IsAutoNumbered { get; }

    /// <summary>
    /// Gets a value indicating whether the table has composite keys.
    /// </summary>
    bool IsCompositeKeyed { get; }

    /// <summary>
    /// Gets the list of columns in the table.
    /// </summary>
    ColumnsList Columns { get; }
}