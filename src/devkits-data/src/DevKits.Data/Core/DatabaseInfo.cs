

namespace DevKits.Data.Core;

/// <summary>
/// Represents information about a database.
/// </summary>
public class DatabaseInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseInfo"/> class.
    /// </summary>
    /// <param name="serverName">The server name.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="connectionString">The connection string.</param>
    public DatabaseInfo(string serverName, string databaseName, string connectionString)
    {
        ServerName = serverName ?? throw new ArgumentNullException(nameof(serverName));
        DatabaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    /// <summary>
    /// Gets or sets the server name.
    /// </summary>
    public string ServerName { get; set; }

    /// <summary>
    /// Gets or sets the database name.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the list of tables in the database.
    /// </summary>
    public TableList Tables { get; set; } = new TableList();

    /// <summary>
    /// Gets or sets the list of schemas in the database.
    /// </summary>
    public SchemaList Schemas { get; set; } = new SchemaList();
}