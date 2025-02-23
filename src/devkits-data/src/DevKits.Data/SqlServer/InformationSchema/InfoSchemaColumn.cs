namespace DevKits.Data.SqlServer.InformationSchema;

using Core;


/// <summary>
/// Represents a column from the INFORMATION_SCHEMA.COLUMNS View.
/// </summary>
public class InfoSchemaColumn
{
    private QualifiedTableColumnName? _qualifiedTableColumnName;

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
    /// Gets or sets the column name.
    /// </summary>
    public string ColumnName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ordinal position of the column.
    /// </summary>
    public int OrdinalPosition { get; set; }

    /// <summary>
    /// Gets or sets the default value of the column.
    /// </summary>
    public string ColumnDefault { get; set; } = null!;

    /// <summary>
    /// Gets or sets the data type of the column.
    /// </summary>
    public string DataType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the declaration of the column.
    /// </summary>
    public string ColumnDeclaration { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether the column is updatable.
    /// </summary>
    public bool IsUpdatable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is computed.
    /// </summary>
    public bool IsComputed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is nullable.
    /// </summary>
    public bool IsNullable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is a ROW GUID COL.
    /// </summary>
    public bool IsRowGuidCol { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is an identity column.
    /// </summary>
    public bool IsIdentity { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is a timestamp column.
    /// </summary>
    public bool IsTimestamp { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is a column set.
    /// </summary>
    public bool IsColumnSet { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is a primary key.
    /// </summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is a unique key.
    /// </summary>
    public bool IsUniqueKey { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is a foreign key.
    /// </summary>
    public bool IsForeignKey { get; set; }

    /// <summary>
    /// Gets or sets the maximum length of the character data type.
    /// </summary>
    public int CharacterMaximumLength { get; set; }

    /// <summary>
    /// Gets or sets the maximum length in bytes of the character data type.
    /// </summary>
    public int CharacterOctetLength { get; set; }

    /// <summary>
    /// Gets or sets the precision of the numeric data type.
    /// </summary>
    public byte NumericPrecision { get; set; }

    /// <summary>
    /// Gets or sets the radix of the numeric data type.
    /// </summary>
    public short NumericPrecisionRadix { get; set; }

    /// <summary>
    /// Gets or sets the scale of the numeric data type.
    /// </summary>
    public int NumericScale { get; set; }

    /// <summary>
    /// Gets or sets the precision of the datetime data type.
    /// </summary>
    public short DatetimePrecision { get; set; }

    /// <summary>
    /// Gets or sets the catalog of the character set.
    /// </summary>
    public string CharacterSetCatalog { get; set; } = null!;

    /// <summary>
    /// Gets or sets the schema of the character set.
    /// </summary>
    public string CharacterSetSchema { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the character set.
    /// </summary>
    public string CharacterSetName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the catalog of the collation.
    /// </summary>
    public string CollationCatalog { get; set; } = null!;

    /// <summary>
    /// Gets or sets the schema of the collation.
    /// </summary>
    public string CollationSchema { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the collation.
    /// </summary>
    public string CollationName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the catalog of the domain.
    /// </summary>
    public string DomainCatalog { get; set; } = null!;

    /// <summary>
    /// Gets or sets the schema of the domain.
    /// </summary>
    public string DomainSchema { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the domain.
    /// </summary>
    public string DomainName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the definition of the computed column.
    /// </summary>
    public string ComputedColumnDefinition { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether the column is generated always.
    /// </summary>
    public bool IsGeneratedAlways { get; set; }

    /// <summary>
    /// Gets or sets the type of the generated always column.
    /// </summary>
    public GenerateAlwaysType GeneratedAlwaysType { get; set; }

    /// <summary>
    /// Gets or sets the qualified table name.
    /// </summary>
    public QualifiedTableName QualifiedTableName { get; set; } = null!;

    /// <summary>
    /// Gets the qualified table column name.
    /// </summary>
    public QualifiedTableColumnName QualifiedTableColumnName => _qualifiedTableColumnName ??= new QualifiedTableColumnName(QualifiedTableName, ColumnName);
}
