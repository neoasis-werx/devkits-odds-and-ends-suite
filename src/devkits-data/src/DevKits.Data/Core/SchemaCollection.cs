namespace DevKits.Data.Core;

using System.Collections.ObjectModel;

/// <summary>
/// Represents a collection of schemas.
/// </summary>
public class SchemaCollection : KeyedCollection<string, SchemaInfo>
{
    /// <summary>
    /// Gets the key for the specified schema.
    /// </summary>
    /// <param name="item">The schema info.</param>
    /// <returns>The schema name.</returns>
    protected override string GetKeyForItem(SchemaInfo item)
    {
        return item.SchemaName;
    }
}