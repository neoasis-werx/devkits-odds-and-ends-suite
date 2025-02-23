namespace DevKits.Data.Abstractions;

using Core;

public interface IColumnInfo
{
    /// <summary>
    /// Gets the qualified table name.
    /// </summary>
    QualifiedTableName TableName { get; }

    /// <summary>
    /// Gets the fully qualified table column name.
    /// </summary>
    ///
    /// <value>The name of the qualified table column.</value>
    QualifiedTableColumnName QualifiedTableColumnName { get; }

    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    string ColumnName { get; }

    /// <summary>
    /// Gets the SQL Quoted column using Brackets
    /// </summary>
    ///
    /// <value>The name of the quoted column.</value>
    string QuotedColumnName => QualifiedTableColumnName.QuotedColumnName;

    /// <summary>
    /// Gets or sets the attributes of the column.
    /// </summary>
    ColumnAttributes Attributes { get; }

    /// <summary>
    /// Data type definition (suitable for DDL).
    /// </summary>
    /// <returns>The full data type specification (including length, precision/scale as applicable)</returns>
    /// <remarks>
    /// Uses column.DbDataType and column.DataType.
    /// When writing full DDL, use the SqlGen DataTypeWriters.
    /// </remarks>
    string? DataTypeDefinition { get; set; }

    /// <summary>
    /// Gets a value indicating whether the column is nullable.
    /// </summary>
    bool IsNullable { get; }

    /// <summary>
    /// Gets a value indicating whether the column is updatable.
    /// </summary>
    bool IsUpdatable { get; }

    /// <summary>
    /// Gets a value indicating whether the column is a primary key.
    /// </summary>
    bool IsPrimaryKey { get; }

    /// <summary>
    /// Gets a value indicating whether the column is a unique key.
    /// </summary>
    bool IsUniqueKey { get; }

    /// <summary>
    /// Gets a value indicating whether the column is an auto-number.
    /// </summary>
    bool IsAutoNumber { get; }

    /// <summary>
    /// Gets a value indicating whether the column is a foreign key.
    /// </summary>
    bool IsForeignKey { get; }

    /// <summary>
    /// Gets a value indicating whether the column is indexed.
    /// </summary>
    bool IsIndexed { get; }

    /// <summary>
    /// Gets a value indicating whether the column is computed.
    /// </summary>
    bool IsComputed { get; }

    /// <summary>
    /// Gets a value indicating whether the column is generated always.
    /// </summary>
    bool IsGeneratedAlways { get; }

    /// <summary>
    /// Gets a value indicating whether the column is generated always at row start.
    /// </summary>
    bool IsGeneratedAlwaysAtRowStart { get; }

    /// <summary>
    /// Gets a value indicating whether the column is generated always at row end.
    /// </summary>
    bool IsGeneratedAlwaysAtRowEnd { get; }

    /// <summary>
    /// Gets a value indicating whether the column is a file stream.
    /// </summary>
    bool IsFileStream { get; }

    /// <summary>
    /// Gets a value indicating whether the column is an xml document.
    /// </summary>
    bool IsXmlDocument { get; }

    /// <summary>
    /// Gets a value indicating whether the column acts as a row guid.
    /// </summary>
    bool IsRowGuidCol { get; }

    /// <summary>
    /// Gets a value indicating whether the column is a column set.
    /// </summary>
    bool IsColumnSet { get; }

    /// <summary>
    /// Gets a value indicating whether the column is a timestamp or a row version.
    /// </summary>
    bool IsTimeStamp { get; }


    bool IsReadonly => !IsUpdatable;
}