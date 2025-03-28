namespace DevKits.Data.Core;

using OddsAndEnds.Core.Collections;

public class DatabaseCollection : KeyedCollectionPlus<string, DatabaseInfo>
{
    #region Overrides of KeyedCollection<QualifiedTableName,TableInfo>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override string GetKeyForItem(DatabaseInfo item)
    {
        return item.DatabaseName;
    }

    #endregion
}