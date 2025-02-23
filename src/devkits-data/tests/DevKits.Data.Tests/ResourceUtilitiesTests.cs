

namespace DevKits.Data.Tests;

using Data.SqlServer.InformationSchema;

[TestFixture()]
public class ResourceUtilitiesTests
{
    [TestCase("SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    public void ResolveFileName_ValidResourceName_ReturnsResolvedFileNameViaNonGeneric(string resourceName, string expected)
    {
        var resolvedFileName = ResourceUtilities.ResolveFileName<InfoSchemata>(resourceName);

        Assert.IsNotNull(resolvedFileName);
        Assert.That(resolvedFileName, Is.EqualTo(expected));
    }


    [TestCase("SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    public void ResolveFileName_ValidResourceName_ReturnsResolvedFileNameViaNonGenericPassType(string resourceName, string expected)
    {
        var resolvedFileName = ResourceUtilities.ResolveFileName(typeof(InfoSchemata), resourceName);

        Assert.IsNotNull(resolvedFileName);
        Assert.That(resolvedFileName, Is.EqualTo(expected));
    }


    [TestCase("SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    public void ResolveFileName_ValidResourceName_ReturnsResolvedFileNameViaStaticType(string resourceName, string expected)
    {
        var resolvedFileName = ResourceUtilities.ResolveFileName(typeof(SQLQueries), resourceName);

        Assert.IsNotNull(resolvedFileName);
        Assert.That(resolvedFileName, Is.EqualTo(expected));
    }

    [TestCase("SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    [TestCase("DevKits.Data\\SqlServer\\InformationSchema\\SelectInfoSchemaTableConstraints.sql", "DevKits.Data.SqlServer.InformationSchema.SelectInfoSchemaTableConstraints.sql")]
    public void ResolveFileName_ValidResourceName_ReturnsEmbeddedResourceFile(string resourceName, string expected)
    {
        var rsrcLoader = ResourceUtilities.GetUtility<InfoSchemata>();
        var resolvedFileName = rsrcLoader.ResolveFileName(resourceName);

        Assert.IsNotNull(resolvedFileName);
        Assert.That(resolvedFileName, Is.EqualTo(expected));
    }

}
