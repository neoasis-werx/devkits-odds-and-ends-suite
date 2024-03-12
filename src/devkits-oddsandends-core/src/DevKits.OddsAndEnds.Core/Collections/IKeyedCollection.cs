namespace DevKits.OddsAndEnds.Core.Collections;

#pragma warning disable CS8767
/// <summary>
/// Represents a generic collection of key/item pairs that are organized by the keys. This interface provides methods for adding, removing,
/// and finding items based on their key and index.
/// </summary>
/// <typeparam name="TKey">The type of the keys in the collection. Keys must be unique and not null.</typeparam>
/// <typeparam name="TItem">The type of the items in the collection.</typeparam>
public interface IKeyedCollection<in TKey, TItem> where TKey : notnull
{
    /// <summary>
    /// Attempts to add an item to the collection if an item with the same key does not already exist.
    /// </summary>
    /// <param name="item">The item to add to the collection. The item's key is extracted internally.</param>
    /// <returns><c>true</c> if the item was added successfully; otherwise, <c>false</c> if an item with the same key already exists.</returns>
    bool TryAdd(TItem item);

    /// <summary>
    /// Adds a range of items to the collection. Each item's key is extracted and checked for uniqueness within the collection.
    /// </summary>
    /// <param name="collection">The collection of items to be added.</param>
    void AddRange(IEnumerable<TItem> collection);

    /// <summary>
    /// Adds an item to the collection. The item's key is extracted and must be unique within the collection.
    /// </summary>
    /// <param name="item">The item to add.</param>
    void Add(TItem item);

    /// <summary>
    /// Removes all items from the collection.
    /// </summary>
    void Clear();

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
    /// Inserts an item to the collection at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which the item should be inserted.</param>
    /// <param name="item">The item to insert.</param>
    void Insert(int index, TItem item);

    /// <summary>
    /// Removes the first occurrence of a specific item from the collection.
    /// </summary>
    /// <param name="item">The item to remove from the collection.</param>
    /// <returns><c>true</c> if item was successfully removed from the collection; otherwise, <c>false</c>. This method also returns <c>false</c> if item is not found in the original collection.</returns>
    bool Remove(TItem item);

    /// <summary>
    /// Removes the item with the specified key from the collection.
    /// </summary>
    /// <param name="key">The key of the item to remove.</param>
    /// <returns><c>true</c> if an item with the specified key was successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if an item with the specified key was not found in the collection.</returns>
    bool Remove(TKey key);

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    void RemoveAt(int index);

    /// <summary>
    /// Gets the number of items contained in the collection.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to get or set.</param>
    /// <returns>The item at the specified index.</returns>
    TItem this[int index] { get; set; }

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