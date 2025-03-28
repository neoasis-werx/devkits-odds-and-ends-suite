namespace DevKits.Data.SqlServer;

using Core;

public class SqlDatabaseSchemaLoader : IDatabaseSchemaLoader
{

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public SqlDatabaseSchemaLoader(string connectionString)
    {
        ConnectionString = connectionString;
    }


    #region Implementation of IDatabaseSchemaLoader

    /// <summary>
    /// Gets or sets the connection string for the database.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Loads the information about the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <returns>The database information.</returns>
    public DatabaseInfo LoadDatabaseInfo(string databaseName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Loads the information about the specified database and tables.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <param name="tables">The qualified table names.</param>
    /// <returns>The database information.</returns>
    public DatabaseInfo LoadDatabaseInfo(string databaseName, params QualifiedTableName[] tables)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Loads the information about the specified table.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <returns>The table information.</returns>
    public TableInfo LoadTableInfo(QualifiedTableName tableName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Loads the list of tables in the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <returns>The list of tables.</returns>
    public TableCollection LoadTables(string databaseName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Loads the list of columns for the specified table.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <returns>The list of columns.</returns>
    public ColumnCollection LoadTableColumns(QualifiedTableName tableName)
    {
        throw new NotImplementedException();
    }

    public Task<DatabaseInfo> LoadDatabaseInfoAsync(string databaseName)
    {
        throw new NotImplementedException();
    }

    public Task<DatabaseInfo> LoadDatabaseInfoAsync(string databaseName, params QualifiedTableName[] tables)
    {
        throw new NotImplementedException();
    }

    public Task<TableInfo> LoadTableInfoAsync(QualifiedTableName tableName)
    {
        throw new NotImplementedException();
    }

    #endregion
}
