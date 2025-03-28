namespace DevKits.Data.SchemaObjects;

/// <summary>
///     Defines the common metadata contract with PascalCase property names.
/// </summary>
public interface ICommonMetadata
{
    /// <summary>
    ///     Catalog (database) of the table or result set source.
    /// </summary>
    string? TableCatalog { get; }

    /// <summary>
    ///     Schema that contains the table or result set source.
    /// </summary>
    string? TableSchema { get; }

    /// <summary>
    ///     Table name or equivalent source name in the metadata.
    /// </summary>
    string? TableName { get; }

    /// <summary>
    ///     Column name in the table or result set.
    /// </summary>
    string? ColumnName { get; }

    /// <summary>
    ///     Ordinal (1-based) position of the column.
    /// </summary>
    int? OrdinalPosition { get; }

    /// <summary>
    ///     Default value of the column, if defined.
    /// </summary>
    string? ColumnDefault { get; }

    /// <summary>
    ///     Nullability of the column: "YES" if nullable, "NO" otherwise.
    /// </summary>
    bool? IsNullable { get; }

    /// <summary>
    ///     Logical/system-supplied data type (e.g., "nvarchar", "int", etc.).
    /// </summary>
    string? DataType { get; }

    /// <summary>
    ///     Maximum length (in characters) for character or binary columns, if applicable.
    /// </summary>
    int? CharacterMaximumLength { get; }

    /// <summary>
    ///     Numeric precision for numeric/decimal columns, if applicable.
    /// </summary>
    byte? NumericPrecision { get; }

    /// <summary>
    ///     Numeric scale for numeric/decimal columns, if applicable.
    /// </summary>
    int? NumericScale { get; }

    /// <summary>
    ///     Subtype code for datetime and SQL-92 interval data types, if applicable.
    /// </summary>
    short? DatetimePrecision { get; }
}