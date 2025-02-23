namespace DevKits.Data.SqlServer.InformationSchema;

using Core;
using OddsAndEnds.Core.Collections;



/// <summary>
/// Represents a collection of <see cref="InfoSchemaColumn"/> objects, keyed by <see cref="QualifiedTableColumnName"/>.
/// </summary>
public class InfoSchemaColumnList : KeyedCollectionPlus<QualifiedTableColumnName, InfoSchemaColumn>
{
    /// <summary>Initializes a new instance of the <see cref="InfoSchemaColumnList"/> class that uses the default equality comparer.</summary>
    /// <param name="columns">The collection of <see cref="InfoSchemaColumn"/> objects to initialize the list with.</param>
    public InfoSchemaColumnList(IEnumerable<InfoSchemaColumn> columns)
    {
        AddRange(columns);
    }

    /// <summary>Initializes a new instance of the <see cref="InfoSchemaColumnList"/> class that uses the default equality comparer.</summary>
    public InfoSchemaColumnList()
    {
    }

    #region Overrides of KeyedCollection<QualifiedTableName,InfoSchemaColumn>

    /// <summary>When implemented in a derived class, extracts the key from the specified element.</summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableColumnName GetKeyForItem(InfoSchemaColumn item)
    {
        return item.QualifiedTableColumnName;
    }

    #endregion
}
