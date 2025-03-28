namespace DevKits.Data.SchemaProviders;

using Core;

public abstract class SchemaProviderBase
{

    protected SchemaProviderBase(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected DatabaseCollection Databases = new();
    protected TableCollection Tables = new();
    protected SchemaCollection Schemas = new();
    protected ColumnCollection Columns = new();


    /// <summary>
    /// Gets or sets the connection string for the database.
    /// </summary>
    public string ConnectionString { get; init; }

    public abstract TableCollection LoadTables();
    public abstract TableCollection LoadTables(string databaseName);

    public abstract SchemaCollection LoadSchemas();
    public abstract DatabaseCollection LoadDatabases();

    public abstract TableInfo LoadTableInfo(QualifiedTableName tableName);
    public abstract ColumnCollection LoadTableColumns(QualifiedTableName tableName);
    public abstract DatabaseInfo LoadDatabaseInfo(string databaseName);
    public abstract DatabaseInfo LoadDatabaseInfo(string databaseName, params QualifiedTableName[] tables);


    public abstract SchemaInfo LoadSchemaInfo(string schemaName);
    public abstract SchemaInfo LoadSchemaInfo(string schemaName, string databaseName);

}
