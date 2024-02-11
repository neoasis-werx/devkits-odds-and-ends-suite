namespace DevKits.Data.Core;

/// <summary>
/// Represents an immutable composite key made up of multiple parts.
/// This class is designed for use as a key in collections like Dictionary or HashSet,
/// where keys are composed of multiple fields or properties.
/// </summary>
public sealed class CompositeKey
{
    private readonly List<object> _keyParts;
    private readonly int _cachedHashCode;

    /// <summary>
    /// Initializes a new instance of the <see cref="CompositeKey"/> class with the specified key parts.
    /// </summary>
    /// <param name="keyParts">The parts that make up the composite key. Each part can be of any type.</param>
    /// <exception cref="ArgumentException">Thrown when keyParts is null or empty.</exception>
    public CompositeKey(params object[] keyParts)
    {
        if (keyParts == null || keyParts.Length == 0)
        {
            throw new ArgumentException("At least one key part must be provided.", nameof(keyParts));
        }

        this._keyParts = new List<object>(keyParts);
        _cachedHashCode = ComputeHashCode();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (CompositeKey)obj;
        if (_keyParts.Count != other._keyParts.Count)
        {
            return false;
        }

        for (var i = 0; i < _keyParts.Count; i++)
        {
            if (!Equals(_keyParts[i], other._keyParts[i]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Serves as the default hash function for the <see cref="CompositeKey"/> class.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => _cachedHashCode;

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string representation of the composite key, consisting of its parts joined by commas.</returns>
    public override string ToString() => string.Join(", ", _keyParts);

    /// <summary>
    /// Computes the hash code for the composite key based on its parts.
    /// </summary>
    /// <returns>The computed hash code.</returns>
    private int ComputeHashCode()
    {
        var hash = new HashCode();
        foreach (var part in _keyParts)
        {
            hash.Add(part);
        }
        return hash.ToHashCode();
    }
}
