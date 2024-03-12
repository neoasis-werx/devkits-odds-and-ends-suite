#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

namespace DevKits.OddsAndEnds.Core.Collections;

using System.Collections.ObjectModel;

public abstract class KeyedCollectionPlus<TKey, TItem> : KeyedCollection<TKey, TItem>, IKeyedCollection<TKey, TItem>, IReadonlyKeyedCollection<TKey, TItem> where TKey : notnull
{
    /// <summary>Initializes a new instance of the <see cref="KeyedCollectionPlus{TKey,Titem}" /> class that uses the default equality comparer.</summary>
    protected KeyedCollectionPlus()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="KeyedCollectionPlus{TKey,Titem}" /> class that uses the specified equality comparer.</summary>
    /// <param name="comparer">The implementation of the <see cref="IEqualityComparer{TKey}" /> generic interface to use when comparing keys, or <see langword="null" /> to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
    protected KeyedCollectionPlus(IEqualityComparer<TKey>? comparer) : base(comparer)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="KeyedCollectionPlus{TKey,Titem}" /> class that uses the specified equality comparer and creates a lookup dictionary when the specified threshold is exceeded.</summary>
    /// <param name="comparer">The implementation of the <see cref="IEqualityComparer{TKey}" /> generic interface to use when comparing keys, or <see langword="null" /> to use the default equality comparer for the type of the key, obtained from <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.</param>
    /// <param name="dictionaryCreationThreshold">The number of elements the collection can hold without creating a lookup dictionary (0 creates the lookup dictionary when the first item is added), or -1 to specify that a lookup dictionary is never created.</param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="dictionaryCreationThreshold" /> is less than -1.</exception>
    protected KeyedCollectionPlus(IEqualityComparer<TKey>? comparer, int dictionaryCreationThreshold) : base(comparer, dictionaryCreationThreshold)
    {
    }


    /// <summary>
    /// Tries to add an item to the <see cref="IKeyedCollection{TKey,TItem}"/> if the item with the same key does not already exist in the collection.
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
    /// Adds a range of items to the <see cref="IKeyedCollection{TKey,TItem}"/>.
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
