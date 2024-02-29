namespace DevKits.OddsAndEnds.Core.Types;

/// <summary>
/// Represents a base class for creating semantic types in a domain model. Semantic types wrap simple System types
/// like <c>int</c>, <c>string</c>, etc., to provide domain-specific meanings and constraints, improving the expressiveness
/// and robustness of the domain model.
/// </summary>
/// <typeparam name="T">The underlying System type that the semantic type is wrapping, adding constraints and specific meaning.</typeparam>
/// <typeparam name="TSelf">The type of the semantic type itself, enabling fluent and type-safe operations.</typeparam>
/// <remarks>
/// <para>
/// Semantic types are an essential part of domain-driven design, offering a way to use primitive types more expressively
/// by wrapping them in more meaningful types. This approach helps avoid primitive obsession and makes the domain model
/// more readable and less error-prone.
/// </para>
/// <para>
/// However, due to C#'s type system limitations, it's not possible to enforce the semantic type to only wrap value types
/// and strings without also allowing other reference types. Therefore, it's recommended that implementers adhere to the
/// convention of only using value types or strings as the underlying type for semantic types.
/// </para>
/// <para>
/// This class also implements <c>IEquatable&lt;TSelf&gt;</c> and <c>IComparable&lt;TSelf&gt;</c> to ensure that semantic types
/// can be compared and equated in a type-safe manner, leveraging the domain-specific meaning encoded in these types.
/// </para>
/// </remarks>
/// <example>
/// Here is how you might define a <c>EmailAddress</c> semantic type using this base class:
/// <code>
/// public class EmailAddress : SemanticType&lt;EmailAddress, string&gt;
/// {
///     public EmailAddress(string value) : base(value)
///     {
///         if (!value.Contains("@"))
///             throw new ArgumentException("Value must be a valid email address", nameof(value));
///     }
/// }
/// </code>
/// This example enforces that the string value passed to the <c>EmailAddress</c> type must contain an "@" symbol,
/// encapsulating the logic for validating email addresses within the type itself.
/// </example>
public abstract class SemanticType<TSelf, T> : IEquatable<TSelf>, IComparable<TSelf>
    where TSelf : SemanticType<TSelf, T>
    where T : IComparable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SemanticType&lt;TSelf, T&gt;"/> class with the specified underlying value.
    /// </summary>
    /// <param name="value">The value of the type instance, which must adhere to the constraints and meaning
    /// defined by the semantic type.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="value"/> is null.</exception>
    protected SemanticType(T value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }

    /// <summary>
    /// Gets the underlying value of the semantic type.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an integer that indicates
    /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the
    /// other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    public virtual int CompareTo(TSelf? other)
        => other is null ? 1 : Value.CompareTo(other.Value);


    /// <summary>
    /// Determines whether the specified semantic type is equal to the current semantic type.
    /// </summary>
    /// <param name="other">The semantic type to compare with the current semantic type.</param>
    /// <returns>true if the specified semantic type is equal to the current semantic type; otherwise, false.</returns>
    /// <remarks>
    /// This method checks for equality by comparing the underlying value of the semantic type. It is important
    /// for ensuring that instances of semantic types with the same value are considered equal, facilitating
    /// their use in collections and in logical operations.
    /// </remarks>
    public virtual bool Equals(TSelf? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return Value.Equals(other.Value);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    /// <remarks>
    /// This override is necessary to ensure that the equality operation behaves correctly in scenarios where
    /// the object being compared may not be of type <typeparamref name="TSelf"/> but could still potentially
    /// be equal to the current instance.
    /// </remarks>
    public override bool Equals(object? obj) => Equals(obj as TSelf);

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    /// <remarks>
    /// The hash code is generated based on the underlying value's hash code. This ensures that semantic types
    /// with the same value will produce the same hash code, aligning with the equality implementation.
    /// </remarks>
    public override int GetHashCode() => Value.GetHashCode();

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    ///
    /// <returns>A string that represents the current object.</returns>
    ///
    /// <seealso cref="System.Object.ToString()"/>
    public override string? ToString()
        => Value.ToString();

    /// <summary>
    /// Equality operator Determines whether two specified instances of SemanticType are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> and <paramref name="right"/> represent the same value; otherwise, false.</returns>
    public static bool operator ==(SemanticType<TSelf, T>? left, SemanticType<TSelf, T>? right)
        => left is null ? right is null : left.Equals(right);  // => Object.Equals(left, right)


    /// <summary>
    /// Inequality operator Determines whether two specified instances of SemanticType are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> and <paramref name="right"/> do not represent the same value; otherwise, false.</returns>
    public static bool operator !=(SemanticType<TSelf, T>? left, SemanticType<TSelf, T>? right)
        => !(left == right);

    /// <summary>
    /// Less-than comparison operator Determines whether one specified SemanticType is less than another specified SemanticType.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator <(SemanticType<TSelf, T>? left, SemanticType<TSelf, T>? right)
        => left is null ? right is not null : left.CompareTo((TSelf?)right) < 0;

    /// <summary>
    /// Less-than-or-equal comparison operator Determines whether one specified SemanticType is less than or equal to another specified SemanticType.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, false.</returns>

    public static bool operator <=(SemanticType<TSelf, T>? left, SemanticType<TSelf, T>? right)
        => left is null || left.CompareTo((TSelf?)right) <= 0;

    /// <summary>
    /// Greater-than comparison operator Determines whether one specified SemanticType is greater than another specified SemanticType.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.</returns>

    public static bool operator >(SemanticType<TSelf, T>? left, SemanticType<TSelf, T>? right)
        => left is not null && left.CompareTo((TSelf?)right) > 0;

    /// <summary>
    /// Greater-than-or-equal comparison operator Determines whether one specified SemanticType is greater than or equal to another specified SemanticType.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator >=(SemanticType<TSelf, T>? left, SemanticType<TSelf, T>? right)
        => left is null ? right is null : left.CompareTo((TSelf?)right) >= 0;


    public static implicit operator T(SemanticType<TSelf, T> obj)
        => obj.Value;
}
