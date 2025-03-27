#pragma warning disable S101

namespace DevKits.Data.SqlServer.InformationSchema;

public static class SQLQueries
{
    public static SQLQuery SelectInfoSchemaTablesRsrc { get; } = ResourceUtilities.ReadTextFile<InfoSchemata>("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTables.sql");
    public static SQLQuery SelectInfoSchemaColumnsRsrc { get; } = ResourceUtilities.ReadTextFile<InfoSchemata>("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaColumns.sql");
    public static SQLQuery SelectInfoSchemaTableConstraintsRsrc { get;  }= ResourceUtilities.ReadTextFile<InfoSchemata>("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTableConstraints.sql");
}
