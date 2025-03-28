using DevKits.Data.Core;

namespace DevKits.Data.Abstractions;

public interface IDatabaseInfo
{
    /// <summary>
    /// Gets or sets the server name.
    /// </summary>
    string ServerName { get; set; }

    /// <summary>
    /// Gets or sets the database name.
    /// </summary>
    string DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the list of tables in the database.
    /// </summary>
    TableSet Tables { get; set; }

    /// <summary>
    /// Gets or sets the list of schemas in the database.
    /// </summary>
    SchemaSet Schemas { get; set; }
}