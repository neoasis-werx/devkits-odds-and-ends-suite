namespace DevKits.OddsAndEnds.Core;

using System.Collections.ObjectModel;

public abstract class KeyedCollectionEx<TKey, TItem> : KeyedCollection<TKey, TItem> where TKey: notnull
{
    /// <summary>
    /// Tries to add an item to the <see cref="KeyedCollectionEx{TKey, TItem}"/> if the item with the same key does not already exist in the collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <returns><c>true</c> if the item was added successfully; otherwise, <c>false</c>.</returns>
    public bool TryAdd(TItem item)
    {
        if (Contains(GetKeyForItem(item)))
            return false;

        Add(item);
        return true;
    }

    /// <summary>
    /// Adds a range of items to the <see cref="KeyedCollectionEx{TKey, TItem}"/>.
    /// </summary>
    /// <param name="collection">The collection of items to add.</param>
    public void AddRange(IEnumerable<TItem> collection)
    {
        foreach (var item in collection)
        {
            TryAdd(item);
        }
    }
}
