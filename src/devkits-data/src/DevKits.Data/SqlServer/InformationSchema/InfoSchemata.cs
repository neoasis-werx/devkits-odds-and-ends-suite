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
    /// <returns>A <see cref="InfoSchemaTableList"/> containing the information schema tables.</returns>
    public InfoSchemaTableList GetInfoSchemaTables(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var results = conn.Query<InfoSchemaTable>(SQLQueries.SelectInfoSchemaTablesRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        return new InfoSchemaTableList(results);
    }

    /// <summary>
    /// Retrieves a list of information schema columns from the SQL Server database.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified name of the table.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <returns>A <see cref="InfoSchemaColumnList"/> containing the information schema columns.</returns>
    public InfoSchemaColumnList GetInfoSchemaColumns(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var results = conn.Query<InfoSchemaColumn>(SQLQueries.SelectInfoSchemaColumnsRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        return new InfoSchemaColumnList(results);
    }

    /// <summary>
    /// Retrieves a hierarchical representation of information schema tables and their columns from the SQL Server database.
    /// </summary>
    /// <param name="qualifiedTableName">The qualified name of the table.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <returns>A <see cref="InfoSchemaTableList"/> containing the information schema tables and their columns.</returns>
    public InfoSchemaTableList GetInfoSchemaTablesTree(string? qualifiedTableName = null, string? schemaName = null)
    {
        using var conn = GetConnection();

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var result = conn.Query<InfoSchemaTable>(SQLQueries.SelectInfoSchemaTablesRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        var infoTables = new InfoSchemaTableList(result);

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

        DefaultTypeMap.MatchNamesWithUnderscores = true;
        var results = conn.Query<InfoSchemaTableConstraint>(SQLQueries.SelectInfoSchemaTableConstraintsRsrc, new { QualifiedTableName = qualifiedTableName, SCHEMA_NAME = schemaName });
        return new List<InfoSchemaTableConstraint>(results);
    }

    private SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}
