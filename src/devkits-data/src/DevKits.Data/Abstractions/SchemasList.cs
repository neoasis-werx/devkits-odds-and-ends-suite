using DevKits.Data.Core;

namespace DevKits.Data.Abstractions;

using System.Collections.ObjectModel;

/// <summary>
/// Represents a collection of schemas.
/// </summary>
public class SchemasList : KeyedCollection<string, ISchemaInfo>
{
    /// <summary>
    /// Gets the key for the specified schema.
    /// </summary>
    /// <param name="item">The schema info.</param>
    /// <returns>The schema name.</returns>
    protected override string GetKeyForItem(ISchemaInfo item)
    {
        return item.SchemaName;
    }
}