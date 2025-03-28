namespace DevKits.OddsAndEnds.Core.Collections;

using System.Collections;
using System.Text.Json;

/// <summary>
///     A generic bidirectional dictionary that allows fast lookups in both directions.
///     Supports read-only mode, serialization, bulk operations, and thread safety.
/// </summary>
/// <typeparam name="TKey">The key type (e.g., C# property name).</typeparam>
/// <typeparam name="TValue">The value type (e.g., SQL column name).</typeparam>
public class BiDirectionalDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TValue : notnull where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _forward;
    private readonly bool _isReadOnly;
    private readonly ReaderWriterLockSlim _lock = new();
    private readonly Dictionary<TValue, TKey> _reverse;


    /// <summary>
    ///     Initializes a new instance of the <see cref="BiDirectionalDictionary{TKey, TValue}" /> class with an optional
    ///     read-only mode.
    /// </summary>
    /// <param name="dictionary">The initial dictionary to populate the bidirectional dictionary.</param>
    /// <param name="isReadOnly">If true, the dictionary is read-only and cannot be modified.</param>
    /// <param name="keyComparer">The comparer to use for the keys, or null to use the default comparer.</param>
    /// <param name="valueComparer">The comparer to use for the values, or null to use the default comparer.</param>
    /// <exception cref="ArgumentException">Thrown when a duplicate value is detected in the initial dictionary.</exception>
    public BiDirectionalDictionary(Dictionary<TKey, TValue> dictionary, bool isReadOnly = false, IEqualityComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null)
    {
        _isReadOnly = isReadOnly;
        _forward = new Dictionary<TKey, TValue>(dictionary, keyComparer ?? EqualityComparer<TKey>.Default);
        _reverse = new Dictionary<TValue, TKey>(valueComparer ?? EqualityComparer<TValue>.Default);

        foreach (var kvp in dictionary)
        {
            if (_reverse.ContainsKey(kvp.Value))
            {
                throw new ArgumentException($"Duplicate value detected in bidirectional mapping: {kvp.Value}");
            }

            _reverse[kvp.Value] = kvp.Key;
        }
    }


    /// <summary>
    ///     Initializes a new instance of the <see cref="BiDirectionalDictionary{TKey, TValue}" /> class with an optional
    ///     read-only mode.
    /// </summary>
    /// <param name="isReadOnly">If true, the dictionary is read-only and cannot be modified.</param>
    /// <param name="keyComparer">The comparer to use for the keys, or null to use the default comparer.</param>
    /// <param name="valueComparer">The comparer to use for the values, or null to use the default comparer.</param>
    public BiDirectionalDictionary(IEqualityComparer<TKey>? keyComparer = null, IEqualityComparer<TValue>? valueComparer = null, bool isReadOnly = false)
    {
        _isReadOnly = isReadOnly;
        _forward = new Dictionary<TKey, TValue>(keyComparer ?? EqualityComparer<TKey>.Default);
        _reverse = new Dictionary<TValue, TKey>(valueComparer ?? EqualityComparer<TValue>.Default);
    }

    /// <summary>
    ///     Gets the number of mappings.
    /// </summary>
    public int Count
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _forward.Count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    /// <summary>
    ///     Iterates over key-value pairs in the dictionary.
    /// </summary>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        _lock.EnterReadLock();
        try
        {
            return _forward.GetEnumerator();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    #region Implementation of IEnumerable

    /// <summary>Returns an enumerator that iterates through a collection.</summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    /// <summary>
    ///     Ensures the dictionary is not in read-only mode before modifying it.
    /// </summary>
    private void EnsureNotReadOnly()
    {
        if (_isReadOnly)
        {
            throw new InvalidOperationException("Cannot modify a read-only BiDirectionalDictionary.");
        }
    }

    /// <summary>
    ///     Gets the value associated with the given key (Forward Lookup).
    /// </summary>
    public TValue Forward(TKey key)
    {
        _lock.EnterReadLock();
        try
        {
            return _forward[key];
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Gets the key associated with the given value (Reverse Lookup).
    /// </summary>
    public TKey Reverse(TValue value)
    {
        _lock.EnterReadLock();
        try
        {
            return _reverse[value];
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Tries to get the value for the given key (Forward Lookup).
    /// </summary>
    public bool TryForward(TKey key, out TValue? value)
    {
        _lock.EnterReadLock();
        try
        {
            return _forward.TryGetValue(key, out value);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Tries to get the key for the given value (Reverse Lookup).
    /// </summary>
    public bool TryReverse(TValue value, out TKey? key)
    {
        _lock.EnterReadLock();
        try
        {
            return _reverse.TryGetValue(value, out key);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Checks if a key exists in the dictionary (Forward Lookup).
    /// </summary>
    public bool ContainsForward(TKey key)
    {
        _lock.EnterReadLock();
        try
        {
            return _forward.ContainsKey(key);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Checks if a value exists in the dictionary (Reverse Lookup).
    /// </summary>
    public bool ContainsReverse(TValue value)
    {
        _lock.EnterReadLock();
        try
        {
            return _reverse.ContainsKey(value);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Adds a new key-value pair to the dictionary.
    /// </summary>
    public void Add(TKey key, TValue value)
    {
        EnsureNotReadOnly();
        _lock.EnterWriteLock();
        try
        {
            if (_forward.ContainsKey(key) || _reverse.ContainsKey(value))
            {
                throw new ArgumentException("Duplicate key or value detected in bidirectional mapping.");
            }

            _forward[key] = value;
            _reverse[value] = key;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    /// <summary>
    ///     Adds a new key-value pair to the dictionary.
    /// </summary>
    private void AddWithNoLock(TKey key, TValue value)
    {
        EnsureNotReadOnly();

        if (_forward.ContainsKey(key) || _reverse.ContainsKey(value))
        {
            throw new ArgumentException("Duplicate key or value detected in bidirectional mapping.");
        }

        _forward[key] = value;
        _reverse[value] = key;
    }


    /// <summary>
    ///     Removes a mapping by key (Forward Lookup).
    /// </summary>
    public bool RemoveByKey(TKey key)
    {
        EnsureNotReadOnly();
        _lock.EnterWriteLock();
        try
        {
            if (_forward.TryGetValue(key, out var value))
            {
                _forward.Remove(key);
                _reverse.Remove(value);
                return true;
            }

            return false;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    /// <summary>
    ///     Removes a mapping by key (Forward Lookup).
    /// </summary>
    private bool RemoveByKeyWithNoLock(TKey key)
    {
        EnsureNotReadOnly();

        if (_forward.TryGetValue(key, out var value))
        {
            _forward.Remove(key);
            _reverse.Remove(value);
            return true;
        }

        return false;
    }


    /// <summary>
    ///     Removes a mapping by value (Reverse Lookup).
    /// </summary>
    public bool RemoveByValue(TValue value)
    {
        EnsureNotReadOnly();
        _lock.EnterWriteLock();
        try
        {
            if (_reverse.TryGetValue(value, out var key))
            {
                _reverse.Remove(value);
                _forward.Remove(key);
                return true;
            }

            return false;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    /// <summary>
    ///     Adds multiple key-value pairs at once.
    /// </summary>
    public void AddRange(Dictionary<TKey, TValue> items)
    {
        EnsureNotReadOnly();
        _lock.EnterWriteLock();
        try
        {
            foreach (var kvp in items)
            {
                AddWithNoLock(kvp.Key, kvp.Value);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    /// <summary>
    ///     Removes multiple keys at once.
    /// </summary>
    public void RemoveByKeys(IEnumerable<TKey> keys)
    {
        EnsureNotReadOnly();
        _lock.EnterWriteLock();
        try
        {
            foreach (var key in keys)
            {
                _ = RemoveByKeyWithNoLock(key);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    /// <summary>
    ///     Serializes the dictionary to JSON format.
    /// </summary>
    public string ToJson()
    {
        _lock.EnterReadLock();
        try
        {
            return JsonSerializer.Serialize(_forward);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    /// <summary>
    ///     Loads a BiDirectionalDictionary from a JSON string.
    /// </summary>
    public static BiDirectionalDictionary<TKey, TValue> FromJson(string json)
    {
        var dictionary = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json);
        return new BiDirectionalDictionary<TKey, TValue>(dictionary ?? throw new InvalidOperationException("Deserialized Dictionary was null."));
    }
}