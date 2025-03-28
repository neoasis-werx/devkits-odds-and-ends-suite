#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using DevKits.Data.Abstractions;

namespace DevKits.Data.SchemaObjects;

using OddsAndEnds.Core.Collections;

/// <summary>
///     Represents column metadata similar to INFORMATION_SCHEMA.COLUMNS,
///     with PascalCase properties and nullable types.
/// </summary>
public class InformationSchemaColumn : ICommonMetadata
{
    /// <summary>
    ///     Catalog (database) of the table.
    /// </summary>
    public string? TableCatalog { get; set; }

    /// <summary>
    ///     Schema that contains the table.
    /// </summary>
    public string? TableSchema { get; set; }

    /// <summary>
    ///     Table name.
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    ///     Column name.
    /// </summary>
    public string? ColumnName { get; set; }

    /// <summary>
    ///     Column identification number.
    /// </summary>
    public int? OrdinalPosition { get; set; }

    /// <summary>
    ///     Default value of the column.
    /// </summary>
    public string? ColumnDefault { get; set; }

    /// <summary>
    ///     Nullability of the column. Returns "YES" if the column allows NULL; otherwise, "NO".
    /// </summary>
    public bool? IsNullable { get; set; }

    /// <summary>
    ///     System-supplied data type.
    /// </summary>
    public string? DataType { get; set; }

    /// <summary>
    ///     Maximum length, in characters, for binary data, character data, or text and image data.
    ///     Otherwise, NULL.
    /// </summary>
    public int? CharacterMaximumLength { get; set; }

    /// <summary>
    ///     Maximum length, in bytes, for binary data, character data, or text and image data.
    ///     Otherwise, NULL.
    /// </summary>
    public int? CharacterOctetLength { get; set; }

    /// <summary>
    ///     Precision of approximate numeric data, exact numeric data, integer data, or monetary data.
    ///     Otherwise, NULL.
    /// </summary>
    public byte? NumericPrecision { get; set; }

    /// <summary>
    ///     Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data.
    ///     Otherwise, NULL.
    /// </summary>
    public short? NumericPrecisionRadix { get; set; }

    /// <summary>
    ///     Scale of approximate numeric data, exact numeric data, integer data, or monetary data.
    ///     Otherwise, NULL.
    /// </summary>
    public int? NumericScale { get; set; }

    /// <summary>
    ///     Subtype code for datetime and ISO interval data types. Otherwise, NULL.
    /// </summary>
    public short? DatetimePrecision { get; set; }

    /// <summary>
    ///     Returns "master", indicating the database in which the character set is located if the column is character data or
    ///     text data type.
    ///     Otherwise, NULL.
    /// </summary>
    public string? CharacterSetCatalog { get; set; }

    /// <summary>
    ///     Always returns NULL.
    /// </summary>
    public string? CharacterSetSchema { get; set; }

    /// <summary>
    ///     Returns the unique name for the character set if the column is character or text data type.
    ///     Otherwise, NULL.
    /// </summary>
    public string? CharacterSetName { get; set; }

    /// <summary>
    ///     Always returns NULL.
    /// </summary>
    public string? CollationCatalog { get; set; }

    /// <summary>
    ///     Always returns NULL.
    /// </summary>
    public string? CollationSchema { get; set; }

    /// <summary>
    ///     Returns the unique name for the collation if the column is character or text data type.
    ///     Otherwise, NULL.
    /// </summary>
    public string? CollationName { get; set; }

    /// <summary>
    ///     If the column is an alias data type, this is the database name in which it was created.
    ///     Otherwise, NULL.
    /// </summary>
    public string? DomainCatalog { get; set; }

    /// <summary>
    ///     If the column is a user-defined data type, returns the name of the schema of the user-defined data type.
    ///     Otherwise, NULL.
    /// </summary>
    public string? DomainSchema { get; set; }

    /// <summary>
    ///     If the column is a user-defined data type, this is the name of the user-defined data type.
    ///     Otherwise, NULL.
    /// </summary>
    public string? DomainName { get; set; }
}


public static class InformationSchemaColumnMappings
{
    /// <summary>
    /// Dictionary mapping C# property names from InformationSchemaColumn to SQL column names in INFORMATION_SCHEMA.COLUMNS.
    /// Uses nameof() for compile-time safety.
    /// </summary>
    public static readonly BiDirectionalDictionary<string, string> PropertyMappings = new(StringComparer.OrdinalIgnoreCase,StringComparer.OrdinalIgnoreCase)
    {
        { nameof(InformationSchemaColumn.TableCatalog), "TABLE_CATALOG" },
        { nameof(InformationSchemaColumn.TableSchema), "TABLE_SCHEMA" },
        { nameof(InformationSchemaColumn.TableName), "TABLE_NAME" },
        { nameof(InformationSchemaColumn.ColumnName), "COLUMN_NAME" },
        { nameof(InformationSchemaColumn.OrdinalPosition), "ORDINAL_POSITION" },
        { nameof(InformationSchemaColumn.ColumnDefault), "COLUMN_DEFAULT" },
        { nameof(InformationSchemaColumn.IsNullable), "IS_NULLABLE" },
        { nameof(InformationSchemaColumn.DataType), "DATA_TYPE" },
        { nameof(InformationSchemaColumn.CharacterMaximumLength), "CHARACTER_MAXIMUM_LENGTH" },
        { nameof(InformationSchemaColumn.CharacterOctetLength), "CHARACTER_OCTET_LENGTH" },
        { nameof(InformationSchemaColumn.NumericPrecision), "NUMERIC_PRECISION" },
        { nameof(InformationSchemaColumn.NumericPrecisionRadix), "NUMERIC_PRECISION_RADIX" },
        { nameof(InformationSchemaColumn.NumericScale), "NUMERIC_SCALE" },
        { nameof(InformationSchemaColumn.DatetimePrecision), "DATETIME_PRECISION" },
        { nameof(InformationSchemaColumn.CharacterSetCatalog), "CHARACTER_SET_CATALOG" },
        { nameof(InformationSchemaColumn.CharacterSetSchema), "CHARACTER_SET_SCHEMA" },
        { nameof(InformationSchemaColumn.CharacterSetName), "CHARACTER_SET_NAME" },
        { nameof(InformationSchemaColumn.CollationCatalog), "COLLATION_CATALOG" },
        { nameof(InformationSchemaColumn.CollationSchema), "COLLATION_SCHEMA" },
        { nameof(InformationSchemaColumn.CollationName), "COLLATION_NAME" },
        { nameof(InformationSchemaColumn.DomainCatalog), "DOMAIN_CATALOG" },
        { nameof(InformationSchemaColumn.DomainSchema), "DOMAIN_SCHEMA" },
        { nameof(InformationSchemaColumn.DomainName), "DOMAIN_NAME" }
    };
}

