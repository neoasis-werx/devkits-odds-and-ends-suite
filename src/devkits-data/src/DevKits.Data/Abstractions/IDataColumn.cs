namespace DevKits.Data.Abstractions;
using Core;

using System;

/// <summary>
/// Defines the basic functionality and properties for a custom data column.
/// </summary>
public interface IDataColumn
{
    /// <summary>
    /// Gets the name of the schema qualified table.
    /// </summary>
    QualifiedTableName QualifiedTableName { get; }

    /// <summary>
    /// Gets the name of the schema qualified table column.
    /// </summary>
    ///
    /// <value>The name of the qualified table column.</value>
    QualifiedTableColumnName QualifiedTableColumnName { get; }

    /// <summary>
    /// Gets or sets the name of the column.
    /// </summary>
    string ColumnName { get; set; }

    /// <summary>
    /// Gets or sets the ordinal.
    /// </summary>
    int Ordinal { get; set; }

    /// <summary>
    /// Gets or sets the data type of the column.
    /// </summary>
    Type DataType { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether null values are allowed in this column for rows that belong to the table
    /// </summary>
    bool AllowDBNull { get; set; }

    /// <summary>
    /// Gets or sets the default value for the column when a new row is created.
    /// </summary>
    object DefaultValue { get; set; }

    /// <summary>
    /// Gets or sets the expression used to calculate the values in this column.
    /// </summary>
    /// <remarks>
    /// Can be used for computing column values or for filtering rows.
    /// </remarks>
    string Expression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column automatically increments the value for new rows added to the table.
    /// </summary>
    bool AutoIncrement { get; set; }

    /// <summary>
    /// Gets or sets the starting value for a column that has its AutoIncrement property set to true. The default is 0.
    /// </summary>
    long AutoIncrementSeed { get; set; }

    /// <summary>
    /// Gets or sets the increment used by a column with its AutoIncrement property set to true.
    /// </summary>
    long AutoIncrementStep { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the column is read-only.
    /// </summary>
    /// <remarks>
    /// A read-only column cannot be modified once the row has been added to the table.
    /// </remarks>
    bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether each value in the column must be unique.
    /// </summary>
    bool Unique { get; set; }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    IDataColumn Clone();
}




