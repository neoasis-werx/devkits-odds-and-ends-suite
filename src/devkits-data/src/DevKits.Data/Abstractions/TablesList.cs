using DevKits.Data.Core;

namespace DevKits.Data.Abstractions;

using OddsAndEnds.Core.Collections;

public class TablesList : KeyedCollectionPlus<QualifiedTableName, ITableInfo>
{

    #region Overrides of KeyedCollection<QualifiedTableName,TableInfo>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableName GetKeyForItem(ITableInfo item)
    {
        return item.TableName;
    }

    #endregion
}