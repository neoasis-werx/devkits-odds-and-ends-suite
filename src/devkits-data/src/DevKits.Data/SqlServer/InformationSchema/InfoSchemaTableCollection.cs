namespace DevKits.Data.SqlServer.InformationSchema;

using Core;
using OddsAndEnds.Core.Collections;

/// <summary>
/// List of information schema tables.
/// </summary>
///
/// <seealso cref="KeyedCollectionPlus{QualifiedTableName,InfoSchemaTable}"/>
public class InfoSchemaTableCollection : KeyedCollectionPlus<QualifiedTableName, InfoSchemaTable>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InfoSchemaTableCollection"/> class that uses the default equality comparer.
    /// </summary>
    public InfoSchemaTableCollection(IEnumerable<InfoSchemaTable> tables)
    {
        AddRange(tables);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InfoSchemaTableCollection"/> class that uses the default equality comparer.
    /// </summary>
    public InfoSchemaTableCollection()
    {
    }

    #region Overrides of KeyedCollection<QualifiedTableName,InfoSchemaTable>

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override QualifiedTableName GetKeyForItem(InfoSchemaTable item)
    {
        return item.QualifiedTableName;
    }

    #endregion
}