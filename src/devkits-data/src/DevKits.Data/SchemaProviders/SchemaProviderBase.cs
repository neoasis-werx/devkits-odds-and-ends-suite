namespace DevKits.Data.SchemaProviders;

using Core;

public abstract class SchemaProviderBase
{

    protected SchemaProviderBase(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected DatabaseList Databases = new();
    protected TableList Tables = new();
    protected SchemaList Schemas = new();
    protected ColumnList Columns = new();


    /// <summary>
    /// Gets or sets the connection string for the database.
    /// </summary>
    public string ConnectionString { get; init; }

    public abstract TableList LoadTables();
    public abstract TableList LoadTables(string databaseName);

    public abstract SchemaList LoadSchemas();
    public abstract DatabaseList LoadDatabases();

    public abstract TableInfo LoadTableInfo(QualifiedTableName tableName);
    public abstract ColumnList LoadTableColumns(QualifiedTableName tableName);
    public abstract DatabaseInfo LoadDatabaseInfo(string databaseName);
    public abstract DatabaseInfo LoadDatabaseInfo(string databaseName, params QualifiedTableName[] tables);


    public abstract SchemaInfo LoadSchemaInfo(string schemaName);
    public abstract SchemaInfo LoadSchemaInfo(string schemaName, string databaseName);

}
