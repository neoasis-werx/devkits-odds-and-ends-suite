namespace DevKits.Data.Core;

/// <summary>
/// Specifies the conditions under which a database column value is automatically generated.
/// </summary>
public enum GenerateAlwaysType
{
    /// <summary>
    /// Indicates that the column value is not automatically generated. The value must be explicitly provided.
    /// </summary>
    NotGeneratedAlways = 0,

    /// <summary>
    /// Indicates that the column value is automatically generated at the start of a row's lifecycle.
    /// This is typically used for system versioning columns where a timestamp is recorded at the row insertion time.
    /// </summary>
    GeneratedAlwaysAtRowStart = 1,

    /// <summary>
    /// Indicates that the column value is automatically generated at the end of a row's lifecycle.
    /// This is commonly used in system versioned temporal tables to mark the time when a row was logically deleted or updated.
    /// </summary>
    GeneratedAlwaysAtRowEnd = 2
}
