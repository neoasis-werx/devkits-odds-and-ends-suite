namespace DevKits.Data.Core;

using OddsAndEnds.Core;

public class TableList : KeyedCollectionEx<QualifiedTableName, TableConstant>
{

    #region Overrides of KeyedCollection<QualifiedTableName,TableConstant>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableName GetKeyForItem(TableConstant item)
    {
        return item.TableName;
    }

    #endregion
}


public class ColumnList : KeyedCollectionEx<QualifiedTableColumnName, ColumnConstant>
{
    #region Overrides of KeyedCollection<QualifiedTableName,TableConstant>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableColumnName GetKeyForItem(ColumnConstant item)
    {
        return item.QualifiedTableColumnName;
    }

    #endregion
}