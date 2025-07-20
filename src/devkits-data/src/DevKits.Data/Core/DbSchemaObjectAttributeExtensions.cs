namespace DevKits.Data.Core;

/// <summary>
///     Provides extension methods for setting or clearing flags of <see cref="ColumnAttributes" /> and
///     <see cref="TableAttributes" />.
/// </summary>
public static class DbSchemaObjectAttributeExtensions
{
    /// <summary>
    ///     Sets or clears a specified flag for a <see cref="ColumnAttributes" /> value.
    /// </summary>
    /// <param name="value">The <see cref="ColumnAttributes" /> value to modify.</param>
    /// <param name="flag">The flag to set or clear within <paramref name="value" />.</param>
    /// <param name="set">If true, the flag is set; if false, the flag is cleared.</param>
    /// <returns>The modified <see cref="ColumnAttributes" /> value with the specified flag set or cleared.</returns>
    /// <remarks>
    ///     This method allows for fluent modification of <see cref="ColumnAttributes" /> values, enabling or disabling
    ///     specific attributes dynamically.
    /// </remarks>
    public static ColumnAttributes Set(ref this ColumnAttributes value, ColumnAttributes flag, bool set)
    {
        if (set)
        {
            value |= flag; // Set the flag
        }
        else
        {
            value &= ~flag; // Clear the flag
        }

        return value;
    }

    /// <summary>
    ///     Sets or clears a specified flag for a <see cref="TableAttributes" /> value.
    /// </summary>
    /// <param name="value">The <see cref="TableAttributes" /> value to modify.</param>
    /// <param name="flag">The flag to set or clear within <paramref name="value" />.</param>
    /// <param name="set">If true, the flag is set; if false, the flag is cleared.</param>
    /// <returns>The modified <see cref="TableAttributes" /> value with the specified flag set or cleared.</returns>
    /// <remarks>
    ///     Similar to the method for <see cref="ColumnAttributes" />, this allows for dynamic adjustments of
    ///     <see cref="TableAttributes" />,
    ///     facilitating the management of table-level attributes in code.
    /// </remarks>
    public static TableAttributes Set(ref this TableAttributes value, TableAttributes flag, bool set)
    {
        if (set)
        {
            value |= flag; // Set the flag
        }
        else
        {
            value &= ~flag; // Clear the flag
        }

        return value;
    }


    public static void SetAttributes(this TableInfo table)
    {
        var attributes = TableAttributes.None;

        //IsHistoryTableForSystemVersionedTable
        attributes.Set(TableAttributes.IsHistoryTableForSystemVersionedTable, table.IsHistoryTableForSystemVersionedTable);

        //IsSystemVersionedTemporalTable
        attributes.Set(TableAttributes.IsSystemVersionedTemporalTable, table.IsSystemVersionedTemporalTable);

        //IsUserTable
        attributes.Set(TableAttributes.IsUserTable, table.IsUserTable);

        //IsUserTableType
        attributes.Set(TableAttributes.IsUserTableType, table.IsUserTableType);

        //IsAutoNumbered
        attributes.Set(TableAttributes.IsAutoNumbered, table.IsAutoNumbered);
        //IsCompositeKeyed
        attributes.Set(TableAttributes.IsCompositeKeyed, table.IsCompositeKeyed);

        table.Attributes = attributes;
    }

    public static void SetAttributes(this ColumnInfo column)
    {
        var attributes = ColumnAttributes.None;

        // IsNullable
        attributes.Set(ColumnAttributes.IsNullable, column.IsNullable);

        // IsUpdatable
        attributes.Set(ColumnAttributes.IsUpdatable, column.IsUpdatable);

        // IsPrimaryKey
        attributes.Set(ColumnAttributes.IsPrimaryKey, column.IsPrimaryKey);

        // IsUniqueKey
        attributes.Set(ColumnAttributes.IsUniqueKey, column.IsUniqueKey);

        // IsAutoNumber
        attributes.Set(ColumnAttributes.IsAutoNumber, column.IsAutoNumber);

        // IsForeignKey
        attributes.Set(ColumnAttributes.IsForeignKey, column.IsForeignKey);

        // IsIndexed
        attributes.Set(ColumnAttributes.IsIndexed, column.IsIndexed);

        // IsComputed
        attributes.Set(ColumnAttributes.IsComputed, column.IsComputed);

        // IsRowGuidColumn
        attributes.Set(ColumnAttributes.IsRowGuidColumn, column.IsRowGuidCol);

        // IsGeneratedAlways
        attributes.Set(ColumnAttributes.IsGeneratedAlways, column.IsGeneratedAlways);

        // IsGeneratedAlwaysAtRowStart
        attributes.Set(ColumnAttributes.IsGeneratedAlwaysAtRowStart, column.IsGeneratedAlwaysAtRowStart);

        // IsGeneratedAlwaysAtRowEnd
        attributes.Set(ColumnAttributes.IsGeneratedAlwaysAtRowEnd, column.IsGeneratedAlwaysAtRowEnd);

        // IsFileStream
        attributes.Set(ColumnAttributes.IsFileStream, column.IsFileStream);

        // IsXmlDocument
        attributes.Set(ColumnAttributes.IsXmlDocument, column.IsXmlDocument);

        // IsColumnSet
        attributes.Set(ColumnAttributes.IsColumnSet, column.IsColumnSet);

        // IsTimeStamp
        attributes.Set(ColumnAttributes.IsTimeStamp, column.IsTimeStamp);

        column.Attributes = attributes;
    }
}