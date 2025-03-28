namespace DevKits.Data.SqlServer.InformationSchema;

using Dapper;

using Microsoft.Data.SqlClient;


/// <summary>
/// Represents a class for retrieving information from the information schema of a SQL Server database.
/// </summary>
public class InfoSchemata
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InfoSchemata"/> class with the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string to the SQL Server database.</param>
    public InfoSchemata(string connectionString)
    {
        ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    /// <summary>
    /// Gets or sets the connection string to the SQL Server database.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Retrieves a list of information schema tables from the SQL Server database.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified name of the table.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <returns>A <see cref="InfoSchemaTableCollection"/> containing the information schema tables.</returns>
    public InfoSchemaTableCollection GetInfoSchemaTables(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        SetupDapper();
        var results = conn.Query<InfoSchemaTable>(SQLQueries.SelectInfoSchemaTablesRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        return new InfoSchemaTableCollection(results);
    }

    /// <summary>
    /// Retrieves a list of information schema columns from the SQL Server database.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified name of the table.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <returns>A <see cref="InfoSchemaColumnCollection"/> containing the information schema columns.</returns>
    public InfoSchemaColumnCollection GetInfoSchemaColumns(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        SetupDapper();
        var results = conn.Query<InfoSchemaColumn>(SQLQueries.SelectInfoSchemaColumnsRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        return new InfoSchemaColumnCollection(results);
    }

    /// <summary>
    /// Retrieves a hierarchical representation of information schema tables and their columns from the SQL Server database.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified name of the table.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <returns>A <see cref="InfoSchemaTableCollection"/> containing the information schema tables and their columns.</returns>
    public InfoSchemaTableCollection GetInfoSchemaTablesTree(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        SetupDapper();
        var result = conn.Query<InfoSchemaTable>(SQLQueries.SelectInfoSchemaTablesRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        var infoTables = new InfoSchemaTableCollection(result);

        var columns = GetInfoSchemaColumns(qualifiedTableName, schemaName);

        foreach (var column in columns)
        {
            if (infoTables.TryGetValue(column.QualifiedTableName, out var table))
            {
                table.Columns.Add(column);
            }
        }

        return infoTables;
    }

    /// <summary>
    /// Retrieves a list of information schema table constraints from the SQL Server database.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified name of the table.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <returns>A <see cref="List{InfoSchemaTableConstraint}"/> containing the information schema table constraints.</returns>
    public List<InfoSchemaTableConstraint> GetInfoSchemaTableConstraints(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        SetupDapper();
        var results = conn.Query<InfoSchemaTableConstraint>(SQLQueries.SelectInfoSchemaTableConstraintsRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        return new List<InfoSchemaTableConstraint>(results);
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }

    private static void SetupDapper()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
