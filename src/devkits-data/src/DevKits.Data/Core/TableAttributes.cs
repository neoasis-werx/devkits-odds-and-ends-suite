namespace DevKits.Data.Core;


/// <summary>
/// Defines attributes that can be applied to a database table.
/// These attributes describe the table's characteristics, such as its type, whether it supports system-versioned temporal features, or if it includes specific key configurations.
/// </summary>
[Flags]
public enum TableAttributes
{
    /// <summary>
    /// Specifies that the table has no special attributes.
    /// </summary>
    None = 0,

    /// <summary>
    /// Indicates that the table is a history table for a system-versioned temporal table.
    /// History tables store old versions of rows from the main table, allowing for data versioning.
    /// </summary>
    IsHistoryTableForSystemVersionedTable = 1,

    /// <summary>
    /// Designates the table as a system-versioned temporal table.
    /// This type of table automatically keeps a full history of data changes, enabling time-based queries.
    /// </summary>
    IsSystemVersionedTemporalTable = 2,

    /// <summary>
    /// Marks the table as a user-defined table, which is not a special system table or type.
    /// </summary>
    IsUserTable = 4,

    /// <summary>
    /// Indicates that the table is a user-defined table type.
    /// Table types are templates for table-valued parameters in stored procedures or functions.
    /// </summary>
    IsUserTableType = 8,

    /// <summary>
    /// Specifies that the table includes an auto-numbered column, often used for generating unique row identifiers.
    /// </summary>
    IsAutoNumbered = 16,

    /// <summary>
    /// Indicates that the table uses a composite key as its primary key.
    /// A composite key consists of two or more columns used together to create a unique identifier for each row.
    /// </summary>
    IsCompositeKeyed = 32,
}
