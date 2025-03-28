using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevKits.Data.Core;
public abstract class DatabaseSchemaLoaderBase : IDatabaseSchemaLoader
{
    protected DatabaseSchemaLoaderBase(string connectionString)
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
    public abstract DatabaseInfo LoadDatabaseInfo(string databaseName);

    /// <summary>
    /// Loads the information about the specified database and tables.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <param name="tables">The qualified table names.</param>
    /// <returns>The database information.</returns>
    public abstract DatabaseInfo LoadDatabaseInfo(string databaseName, params QualifiedTableName[] tables);

    /// <summary>
    /// Loads the information about the specified table.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <returns>The table information.</returns>
    public abstract TableInfo LoadTableInfo(QualifiedTableName tableName);

    /// <summary>
    /// Loads the list of tables in the specified database.
    /// </summary>
    /// <param name="databaseName">The name of the database.</param>
    /// <returns>The list of tables.</returns>
    public abstract TableCollection LoadTables(string databaseName);

    /// <summary>
    /// Loads the list of columns for the specified table.
    /// </summary>
    /// <param name="tableName">The qualified table name.</param>
    /// <returns>The list of columns.</returns>
    public abstract ColumnCollection LoadTableColumns(QualifiedTableName tableName);

    #endregion
}
