using DevKits.Data.Core;

namespace DevKits.Data;

/// <summary>
/// Represents a constant for a database table.
/// </summary>
public class TableConstant
{
    /// <summary>
    /// Gets or sets the qualified table name.
    /// </summary>
    public QualifiedTableName TableName { get; set; }

    /// <summary>
    /// Gets the attributes of the table.
    /// </summary>
    public TableAttributes Attributes { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the table is a history table for a system-versioned table.
    /// </summary>
    public bool IsHistoryTableForSystemVersionedTable => Attributes.HasFlag(TableAttributes.IsHistoryTableForSystemVersionedTable);

    /// <summary>
    /// Gets a value indicating whether the table is a system-versioned temporal table.
    /// </summary>
    public bool IsSystemVersionedTemporalTable => Attributes.HasFlag(TableAttributes.IsSystemVersionedTemporalTable);

    /// <summary>
    /// Gets a value indicating whether the table is a user table.
    /// </summary>
    public bool IsUserTable => Attributes.HasFlag(TableAttributes.IsUserTable);

    /// <summary>
    /// Gets a value indicating whether the table is a user table type.
    /// </summary>
    public bool IsUserTableType => Attributes.HasFlag(TableAttributes.IsUserTableType);

    /// <summary>
    /// Gets a value indicating whether the table has auto-numbered columns.
    /// </summary>
    public bool IsAutoNumbered => Attributes.HasFlag(TableAttributes.IsAutoNumbered);

    /// <summary>
    /// Gets a value indicating whether the table has composite keys.
    /// </summary>
    public bool IsCompositeKeyed => Attributes.HasFlag(TableAttributes.IsCompositeKeyed);

    /// <summary>
    /// Gets the list of columns in the table.
    /// </summary>
    public IReadOnlyList<ColumnConstant> Columns { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TableConstant"/> class.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified table name.</param>
    /// <param name="attributes">The attributes of the table.</param>
    /// <param name="columnNames">The names and attributes of the columns.</param>
    public TableConstant(string qualifiedTableName, int attributes, params (string ColumnName, int attributes)[] columnNames)
    {
        TableName = new QualifiedTableName(qualifiedTableName);
        Attributes = (TableAttributes)attributes;

        var columnList = new List<ColumnConstant>();

        foreach (var column in columnNames)
        {
            columnList.Add(new ColumnConstant(TableName, column.ColumnName, (ColumnAttributes)column.attributes));
        }

        Columns = columnList;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TableConstant"/> class.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified table name.</param>
    /// <param name="attributes">The attributes of the table.</param>
    /// <param name="columnNames">The names and attributes of the columns.</param>
    public TableConstant(string qualifiedTableName, int attributes, params (string ColumnName, int attributes, string dataTypeDefinition)[] columnNames)
    {
        TableName = new QualifiedTableName(qualifiedTableName);
        Attributes = (TableAttributes)attributes;

        var columnList = new List<ColumnConstant>();

        foreach (var column in columnNames)
        {
            columnList.Add(new ColumnConstant(TableName, column.ColumnName, (ColumnAttributes)column.attributes, column.dataTypeDefinition));
        }

        Columns = columnList;
    }


    /// <summary>
    /// Initializes a new instance of the <see cref="TableConstant"/> class.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified table name.</param>
    /// <param name="attributes">The attributes of the table.</param>
    /// <param name="columnNames">The names and updatable flags of the columns.</param>
    public TableConstant(string qualifiedTableName, int attributes, params (string ColumnName, bool IsUpdatable)[] columnNames)
    {
        TableName = new QualifiedTableName(qualifiedTableName);
        Attributes = (TableAttributes)attributes;

        var columnList = new List<ColumnConstant>();

        foreach (var (columnName, isUpdatable) in columnNames)
        {
            columnList.Add(new ColumnConstant(TableName, columnName, isUpdatable));
        }

        Columns = columnList;
    }

    /// <summary>
    /// Implicitly converts a <see cref="TableConstant"/> to a <see cref="QualifiedTableName"/>.
    /// </summary>
    /// <param name="source">The source <see cref="TableConstant"/>.</param>
    public static implicit operator QualifiedTableName(TableConstant source)
    {
        return source.TableName;
    }


    public override string ToString() => TableName;
}
