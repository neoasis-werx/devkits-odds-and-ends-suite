namespace DevKits.Data.Core;

using OddsAndEnds.Core;

public class TableList : KeyedCollectionEx<QualifiedTableName, TableInfo>
{

    #region Overrides of KeyedCollection<QualifiedTableName,TableInfo>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableName GetKeyForItem(TableInfo item)
    {
        return item.TableName;
    }

    #endregion
}


public class ColumnList : KeyedCollectionEx<QualifiedTableColumnName, ColumnInfo>
{
    #region Overrides of KeyedCollection<QualifiedTableName,TableInfo>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableColumnName GetKeyForItem(ColumnInfo item)
    {
        return item.QualifiedTableColumnName;
    }

    #endregion
}