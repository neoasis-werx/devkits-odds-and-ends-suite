namespace DevKits.Data.Core;

using System.Diagnostics.CodeAnalysis;
using System.Text;
using OddsAndEnds.Core.Text;

/// <summary>
/// Represents a database object identifier, encapsulating the parts of the identifier such as schema, table, and column names,
/// and optionally external parts for references to objects in other databases or servers. This class is designed to handle
/// complex identifiers with multiple parts and provide functionality for encoding and comparing these identifiers.
/// </summary>
[Serializable]
public sealed class DbObjectIdentifier : IEquatable<DbObjectIdentifier>
{
    internal int Id { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbObjectIdentifier"/> class from a variable number of identifier parts.
    /// </summary>
    /// <param name="parts">The parts of the identifier, such as schema and table names.</param>
    public DbObjectIdentifier(params string[] parts)
      : this((IList<string>)parts)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbObjectIdentifier"/> class from a list of identifier parts.
    /// </summary>
    /// <param name="parts">The parts of the identifier as a list.</param>
    public DbObjectIdentifier(IList<string>? parts)
    {
        if (parts == null) return;
        Parts = new List<string>(parts);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbObjectIdentifier"/> class from an enumerable of identifier parts.
    /// </summary>
    /// <param name="parts">The parts of the identifier as an enumerable.</param>
    public DbObjectIdentifier(IEnumerable<string>? parts)
    {
        if (parts == null) return;
        Parts = new List<string>(parts);
    }

    public DbObjectIdentifier(ISchemaObjectName? schemaObjectName)
    {
        if (schemaObjectName == null) return;
        Parts = new List<string>() { schemaObjectName.SchemaName, schemaObjectName.ObjectName };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbObjectIdentifier"/> class from both external parts and regular identifier parts.
    /// External parts are used for references to objects in other databases or servers.
    /// </summary>
    /// <param name="externalParts">External reference parts, used in 3 or 4 part references.</param>
    /// <param name="parts">The regular parts of the identifier.</param>
    public DbObjectIdentifier(IList<string>? externalParts, IList<string>? parts)
    {
        if (externalParts != null && externalParts.Count > 0) ExternalParts = new List<string>(externalParts);

        if (parts == null) return;

        Parts = new List<string>(parts);
    }

    /// <summary>Gets the parts of the identifier.</summary>
    /// <returns>The parts of the identifier.</returns>
    public IList<string>? Parts { get; set; }

    /// <summary>
    /// Does this DbObjectIdentifier have any <see cref="DbObjectIdentifier.Parts" />
    /// </summary>
    public bool HasName => Parts != null && Parts.Count > 0;

    /// <summary>
    /// Gets the external reference parts.  Null if the identifier is not external.
    /// </summary>
    /// <returns>The external reference parts.</returns>
    public IList<string>? ExternalParts { get; set; }

    /// <summary>
    /// Does this DbObjectIdentifier have any <see cref="DbObjectIdentifier.ExternalParts" />
    /// </summary>
    public bool HasExternalParts => ExternalParts != null && ExternalParts.Count > 0;

    /// <summary>Formats and escapes name parts into a single string</summary>
    /// <returns>escaped name.</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        PrintParts(ExternalParts, builder);
        PrintParts(Parts, builder);
        return builder.ToString();
    }



    private static void PrintParts(IList<string>? parts, StringBuilder builder)
    {
        if (parts == null)  return;
        foreach (string part in parts)
        {
            if (builder.Length != 0)
                builder.Append('.');
            builder.Append(EncodeIdentifier(part));
        }
    }

    private const string EscapedRSquareParen = "]]";

    private const string LSquareParen = "[";
    private const string RSquareParen = "]";


    private static string EncodeIdentifier(string identifier)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(LSquareParen);
        // Escape the special character
        builder.Append(identifier.Replace(RSquareParen, EscapedRSquareParen));
        builder.Append(RSquareParen);
        return builder.ToString();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType() || Parts is null)
        {
            return false;
        }

        var other = (DbObjectIdentifier)obj;
        return this.Equals(other);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public bool Equals(DbObjectIdentifier? obj)
    {
        if (obj == null || obj.GetType() != GetType() || Parts is null)
        {
            return false;
        }

        var other = obj;
        if (other.Parts != null && Parts.Count != other.Parts.Count)
        {
            return false;
        }

        return TSQLRosetta.AreEquals(this.ExternalParts, other.ExternalParts) && TSQLRosetta.AreEquals(this.Parts, other.Parts);
    }


    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        int hash =  CaseInsensitiveHashCode.Combine(ExternalParts);
        int partsHashCode = CaseInsensitiveHashCode.Combine(Parts);
        return HashCode.Combine(hash, partsHashCode);
    }

    #region Equality members

    /// <summary>Returns a value that indicates whether the values of two <see cref="DbObjectIdentifier" /> objects are equal.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
    public static bool operator ==(DbObjectIdentifier? left, DbObjectIdentifier? right)
    {
        return Equals(left, right);
    }

    /// <summary>Returns a value that indicates whether two <see cref="DbObjectIdentifier" /> objects have different values.</summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
    public static bool operator !=(DbObjectIdentifier? left, DbObjectIdentifier? right)
    {
        return !Equals(left, right);
    }

    #endregion
}