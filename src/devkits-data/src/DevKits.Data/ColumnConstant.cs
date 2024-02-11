using DevKits.Data.Core;

namespace DevKits.Data;


/// <summary>
/// Represents a constant for a database column.
/// </summary>
public class ColumnConstant
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnConstant"/> class.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="isUpdatable">Specifies whether the column is updatable.</param>
    public ColumnConstant(QualifiedTableName tableName, string columnName, bool isUpdatable)
    {
        this.TableName = tableName;
        this.ColumnName = columnName;
        if (isUpdatable)
        {
            this.Attributes |= ColumnAttributes.IsUpdatable;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnConstant"/> class.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="attributes">The attributes of the column.</param>
    public ColumnConstant(QualifiedTableName tableName, string columnName, ColumnAttributes attributes)
    {
        this.TableName = tableName;
        this.ColumnName = columnName;
        this.Attributes = attributes;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ColumnConstant"/> class.
    /// </summary>
    ///
    /// <param name="tableName">The qualified table name.</param>
    /// <param name="columnName">The name of the column.</param>
    /// <param name="attributes">The attributes of the column.</param>
    /// <param name="dataTypeDefinition">Data type definition (suitable for DDL).</param>
    public ColumnConstant(QualifiedTableName tableName, string columnName, ColumnAttributes attributes, string dataTypeDefinition)
    {
        this.TableName = tableName;
        this.ColumnName = columnName;
        this.Attributes = attributes;
        this.DataTypeDefinition = dataTypeDefinition;
    }


    /// <summary>
    /// Gets the qualified table name.
    /// </summary>
    public QualifiedTableName TableName { get; }

    /// <summary>
    /// Gets the fully qualified table column name.
    /// </summary>
    ///
    /// <value>The name of the qualified table column.</value>
    public QualifiedTableColumnName QualifiedTableColumnName => new(TableName, ColumnName);

    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    public string ColumnName { get; }

    /// <summary>
    /// Gets the SQL Quoted column using Brackets
    /// </summary>
    ///
    /// <value>The name of the quoted column.</value>
    public string QuotedColumnName => TSQLRosetta.QuoteName(ColumnName)!;


    /// <summary>
    /// Gets or sets the attributes of the column.
    /// </summary>
    public ColumnAttributes Attributes { get; private set; } = ColumnAttributes.None;

    /// <summary>
    /// Data type definition (suitable for DDL).
    /// </summary>
    /// <returns>The full data type specification (including length, precision/scale as applicable)</returns>
    /// <remarks>
    /// Uses column.DbDataType and column.DataType.
    /// When writing full DDL, use the SqlGen DataTypeWriters.
    /// </remarks>
    public string? DataTypeDefinition { get; set; }
    /// <summary>
    /// Gets a value indicating whether the column is nullable.
    /// </summary>
    public bool IsNullable => Attributes.HasFlag(ColumnAttributes.IsNullable);

    /// <summary>
    /// Gets a value indicating whether the column is updatable.
    /// </summary>
    public bool IsUpdatable => Attributes.HasFlag(ColumnAttributes.IsUpdatable);

    /// <summary>
    /// Gets a value indicating whether the column is a primary key.
    /// </summary>
    public bool IsPrimaryKey => Attributes.HasFlag(ColumnAttributes.IsPrimaryKey);

    /// <summary>
    /// Gets a value indicating whether the column is a unique key.
    /// </summary>
    public bool IsUniqueKey => Attributes.HasFlag(ColumnAttributes.IsUniqueKey);

    /// <summary>
    /// Gets a value indicating whether the column is an auto-number.
    /// </summary>
    public bool IsAutoNumber => Attributes.HasFlag(ColumnAttributes.IsAutoNumber);

    /// <summary>
    /// Gets a value indicating whether the column is a foreign key.
    /// </summary>
    public bool IsForeignKey => Attributes.HasFlag(ColumnAttributes.IsForeignKey);

    /// <summary>
    /// Gets a value indicating whether the column is indexed.
    /// </summary>
    public bool IsIndexed => Attributes.HasFlag(ColumnAttributes.IsIndexed);

    /// <summary>
    /// Gets a value indicating whether the column is computed.
    /// </summary>
    public bool IsComputed => Attributes.HasFlag(ColumnAttributes.IsComputed);

    /// <summary>
    /// Gets a value indicating whether the column is generated always.
    /// </summary>
    public bool IsGeneratedAlways => Attributes.HasFlag(ColumnAttributes.IsGeneratedAlways);

    /// <summary>
    /// Gets a value indicating whether the column is generated always at row start.
    /// </summary>
    public bool IsGeneratedAlwaysAtRowStart => Attributes.HasFlag(ColumnAttributes.IsGeneratedAlwaysAtRowStart);

    /// <summary>
    /// Gets a value indicating whether the column is generated always at row end.
    /// </summary>
    public bool IsGeneratedAlwaysAtRowEnd => Attributes.HasFlag(ColumnAttributes.IsGeneratedAlwaysAtRowEnd);

    /// <summary>
    /// Gets a value indicating whether the column is a file stream.
    /// </summary>
    public bool IsFileStream => Attributes.HasFlag(ColumnAttributes.IsFileStream);

    /// <summary>
    /// Gets a value indicating whether the column is an xml document.
    /// </summary>
    public bool IsXmlDocument => Attributes.HasFlag(ColumnAttributes.IsXmlDocument);

    /// <summary>
    /// Gets a value indicating whether the column acts as a row guid.
    /// </summary>
    public bool IsRowGuidCol => Attributes.HasFlag(ColumnAttributes.IsRowGuidColumn);

    /// <summary>
    /// Gets a value indicating whether the column is a column set.
    /// </summary>
    public bool IsColumnSet => Attributes.HasFlag(ColumnAttributes.IsColumnSet);

    /// <summary>
    /// Gets a value indicating whether the column is a timestamp or a row version.
    /// </summary>
    public bool IsTimeStamp => Attributes.HasFlag(ColumnAttributes.IsTimeStamp);


    public override string ToString()
    {
        return this.ColumnName;
    }
}