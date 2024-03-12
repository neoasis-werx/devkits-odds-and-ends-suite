namespace DevKits.OddsAndEnds.Core.Collections;

#pragma warning disable CS8767
public interface IReadonlyKeyedCollection<in TKey, TItem> where TKey : notnull
{
    /// <summary>
    /// Determines whether the collection contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate in the collection.</param>
    /// <returns><c>true</c> if item is found in the collection; otherwise, <c>false</c>.</returns>
    bool Contains(TItem item);

    /// <summary>
    /// Determines whether the collection contains an item with the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the collection.</param>
    /// <returns><c>true</c> if an item with the key is found in the collection; otherwise, <c>false</c>.</returns>
    bool Contains(TKey key);

    /// <summary>
    /// Copies the elements of the collection to an Array, starting at a particular Array index.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from collection. The Array must have zero-based indexing.</param>
    /// <param name="index">The zero-based index in array at which copying begins.</param>
    void CopyTo(TItem[] array, int index);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    IEnumerator<TItem> GetEnumerator();

    /// <summary>
    /// Determines the index of a specific item in the collection.
    /// </summary>
    /// <param name="item">The item to locate in the collection.</param>
    /// <returns>The index of the item if found in the collection; otherwise, -1.</returns>
    int IndexOf(TItem item);

    /// <summary>
    /// Gets the number of items contained in the collection.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to get or set.</param>
    /// <returns>The item at the specified index.</returns>
    TItem this[int index] { get; }

    /// <summary>
    /// Gets the item associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the item to get.</param>
    /// <returns>The item associated with the specified key. If the key is not found, an exception is thrown.</returns>
    TItem this[TKey key] { get; }

    /// <summary>
    /// Tries to get the value associated with the specified key from the collection.
    /// </summary>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="item">When this method returns, contains the object from the collection that has the specified key, or the default value of the type if the operation failed.</param>
    /// <returns><c>true</c> if the collection contains an element with the specified key; otherwise, <c>false</c>.</returns>
    bool TryGetValue(TKey key, out TItem item);
}