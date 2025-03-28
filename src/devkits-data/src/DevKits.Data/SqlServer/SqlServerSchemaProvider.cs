namespace DevKits.Data.SqlServer;

using Core;
using Microsoft.Data.SqlClient;
using SchemaProviders;

public class SqlServerSchemaProvider : SchemaProviderBase
{
    private readonly SqlConnectionStringBuilder _connectionStringBuilder;

    public SqlServerSchemaProvider(string connectionString) : base(connectionString)
    {
        _connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        if (_connectionStringBuilder.InitialCatalog != null)
        {
            DatabaseInfo db = new DatabaseInfo(_connectionStringBuilder.DataSource, _connectionStringBuilder.InitialCatalog, connectionString);
            this.Databases.Add(db);
        }
        else
        {
            DatabaseInfo db = new DatabaseInfo(_connectionStringBuilder.DataSource, "master", connectionString);
            this.Databases.Add(db);
        }
    }



    #region Overrides of SchemaProviderBase

    public override TableCollection LoadTables()
    {
        throw new NotImplementedException();
    }

    public override SchemaCollection LoadSchemas()
    {
        throw new NotImplementedException();
    }

    public override DatabaseCollection LoadDatabases()
    {
        throw new NotImplementedException();
    }

    public override TableInfo LoadTableInfo(QualifiedTableName tableName)
    {
        throw new NotImplementedException();
    }

    public override ColumnCollection LoadTableColumns(QualifiedTableName tableName)
    {
        throw new NotImplementedException();
    }

    public override DatabaseInfo LoadDatabaseInfo(string databaseName)
    {
        throw new NotImplementedException();
    }

    public override DatabaseInfo LoadDatabaseInfo(string databaseName, params QualifiedTableName[] tables)
    {
        throw new NotImplementedException();
    }

    public override TableCollection LoadTables(string databaseName)
    {
        throw new NotImplementedException();
    }

    public override SchemaInfo LoadSchemaInfo(string schemaName)
    {
        throw new NotImplementedException();
    }

    public override SchemaInfo LoadSchemaInfo(string schemaName, string databaseName)
    {
        throw new NotImplementedException();
    }

    #endregion
}
