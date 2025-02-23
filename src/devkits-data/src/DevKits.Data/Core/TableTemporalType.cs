namespace DevKits.Data.Core;


/// <summary>
/// Specifies the type of table in SQL Server.
/// </summary>
public enum TableTemporalType
{
    /// <summary>
    /// A non-temporal table.
    /// </summary>
    NonTemporalTable = 0,

    /// <summary>
    /// A history table for a system-versioned temporal table.
    /// </summary>
    HistoryTableForSystemVersionedTable = 1,

    /// <summary>
    /// A system-versioned temporal table.
    /// </summary>
    SystemVersionedTemporalTable = 2
}
