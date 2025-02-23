namespace DevKits.Data.Core;


/// <summary>
/// Represents an interface for loading database schema information.
/// </summary>
public interface IDatabaseSchemaLoader
{
    /// <summary>
    /// Gets or sets the connection string for the database.
    /// </summary>
    string ConnectionString { get; set; }

    /// <summary>
    /// Loads the information about the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <returns>The database information.</returns>
    DatabaseInfo LoadDatabaseInfo(string databaseName);

    /// <summary>
    /// Loads the information about the specified database and tables.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <param name="tables">The qualified table names.</param>
    /// <returns>The database information.</returns>
    DatabaseInfo LoadDatabaseInfo(string databaseName, params QualifiedTableName[] tables);

    /// <summary>
    /// Loads the information about the specified table.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <returns>The table information.</returns>
    TableInfo LoadTableInfo(QualifiedTableName tableName);

    /// <summary>
    /// Loads the list of tables in the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <returns>The list of tables.</returns>
    TableList LoadTables(string databaseName);

    /// <summary>
    /// Loads the list of columns for the specified table.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <returns>The list of columns.</returns>
    ColumnList LoadTableColumns(QualifiedTableName tableName);
}

