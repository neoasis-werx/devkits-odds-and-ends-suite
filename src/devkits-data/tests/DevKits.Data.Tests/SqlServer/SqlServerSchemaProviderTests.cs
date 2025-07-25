﻿
namespace DevKits.Data.Tests.SqlServer;

using Data.SqlServer;
using Data.SqlServer.InformationSchema;

using Databases.ChiLaborProductivity;

using OddsAndEnds.Diagnostic.Tools;

using System.Text;
using Microsoft.Data.SqlClient;

[TestFixture]
public class SqlServerSchemaProviderTests
{
    [Test()]
    public void SqlServerSchemaProviderTest()
    {
        var expected = "Server=localhost;Database=master;Integrated Security=True;";
        var sqlSchemaProvider = new SqlServerSchemaProvider(expected);
        Assert.That(sqlSchemaProvider,Is.Not.Null);
        Assert.That(sqlSchemaProvider.ConnectionString, Is.EqualTo(expected));
    }

    [Test()]
    public void LoadTablesTest()
    {
        var laborTables = LaborProductivity.GetTables();
        foreach (var table in laborTables.Where(t => !t.IsHistoryTableForSystemVersionedTable))
        {
            var sb = new StringBuilder();

            Console.WriteLine(table);
            foreach (var column in table.Columns.Where(c => c.IsUpdatable))
            {
                Console.WriteLine($"--> {column} {column.DataTypeDefinition}");
            }
        }
    }


    [Test]
    public void InfoSchemata_TestLoadTables()
    {
        var builder = new SqlConnectionStringBuilder();

        var CHI_THI_DATABASE = "DCM_chi_tlp_Alomere_PROC";
        var UserName = "d.mccord@craneware.com";
        builder.ConnectionString = $"Server=tcp:dev-sql-thi-1-rocya.database.windows.net,1433;Initial Catalog={CHI_THI_DATABASE};Persist Security Info=False;User ID={UserName};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=\"Active Directory Interactive\";";
        builder.Authentication = SqlAuthenticationMethod.ActiveDirectoryInteractive;
        Console.WriteLine(builder.ConnectionString);

        var schemata = new InfoSchemata(builder.ConnectionString);

        var tables = schemata.GetInfoSchemaTablesTree();
        tables.DumpViz();
    }


    [Test]
    public void SQLQueries_TestGetFromResources()
    {
        var t =  typeof(InfoSchemata).DumpViz();


        //ResourceUtilities.GetResourceNames().DumpViz();
        //ResourceUtilities.ReadTextFile("DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaColumns.sql").DumpViz();

        ResourceUtilities.GetResourceNames<InfoSchemata>().DumpViz();
        var sql = ResourceUtilities.ReadTextFile<InfoSchemata>("SelectInfoSchemaTables.sql").DumpViz();

    }
}