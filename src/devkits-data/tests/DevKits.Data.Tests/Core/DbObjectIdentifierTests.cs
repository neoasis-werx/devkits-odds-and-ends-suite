namespace DevKits.Data.Core.Tests;

using TestTools;

[TestFixture()]
public class DbObjectIdentifierTests
{

    internal static DbObjectIdentifier TestOid01 = new("schemaName", "tableName01");
    internal static IList<string> ExampleTableParts = new List<string>() { "schemaName", "tableName01" };

    internal static IList<string> DcmProductivityExtParts = new List<string>() { "DCM_thi_productivity_Impl" };
    internal static IList<string> ThiProductivityExtParts = new List<string>() { "thi_productivity" };

    internal static IList<string> EmptyList = new List<string>();

    [Test()]
    public void DbObjectIdentifier_EnumerableTest()
    {
        Assert.DoesNotThrow(() => { _ = new DbObjectIdentifier(ExampleTableParts.AsEnumerable()); });
        Assert.DoesNotThrow(() =>
        {
            _ = new DbObjectIdentifier(Enumerable.Empty<string>()); // NOTE: THIS IS REALLY NOT USEFUL TO BE EMPTY.
        });
    }

    [Test()]
    public void DbObjectIdentifier_ListTest()
    {
        Assert.DoesNotThrow(() =>
        {
            _ = new DbObjectIdentifier(ExampleTableParts);
        });

        Assert.DoesNotThrow(() =>
        {
            _ = new DbObjectIdentifier(EmptyList); // NOTE: THIS IS REALLY NOT USEFUL TO BE EMPTY.
        });
    }

    [Test()]
    public void DbObjectIdentifier_ExternalAndParts()
    {
        DbObjectIdentifier? actual = null!;

        Assert.DoesNotThrow(() => { actual = new DbObjectIdentifier(DcmProductivityExtParts, ExampleTableParts); });
        Assert.That(actual?.ExternalParts?.Count, Is.EqualTo(1));
        Assert.That(actual?.Parts?.Count, Is.EqualTo(2));

        DbObjectIdentifier? actual2 = null!;
        Assert.DoesNotThrow(() => { actual2 = new DbObjectIdentifier(DcmProductivityExtParts, EmptyList); });
        Assert.That(actual2?.ExternalParts?.Count, Is.EqualTo(1));
        Assert.That(actual2?.Parts?.Count, Is.EqualTo(0));

        Assert.DoesNotThrow(() => { _ = new DbObjectIdentifier(EmptyList, EmptyList); });
    }

    [Test()]
    public void DbObjectIdentifier_ParamArrayTests()
    {
        DbObjectIdentifier? actual = null;
        Assert.DoesNotThrow(() => { actual = new DbObjectIdentifier("schemaName", "TableName"); });
        Assert.That(actual?.Parts, Is.Not.Null);
        Assert.That(actual?.Parts?.Count, Is.EqualTo(2));
    }

    [Test()]
    public void ToStringTest()
    {
        var oid = new DbObjectIdentifier("schemaName", "TableName");
        Console.WriteLine(oid.ToString());

        Assert.That(oid.ToString(), Is.EqualTo("[schemaName].[TableName]"));


        Console.WriteLine(oid.ToString());
    }

    [Test()]
    public void Equals_LocalOidTest()
    {
        var source = new DbObjectIdentifier("schemaName", "tableName01");
        var other = new DbObjectIdentifier("schemaName", "tableName01");

        Assert.That(source, Is.Not.SameAs(other));
        EqualityTester.TestEqualObjects(source, other);
    }

    [Test()]
    public void Equals_LocalOidCaseInsesitiveTest()
    {
        var source = new DbObjectIdentifier("schemaName", "tableName01");
        var other = new DbObjectIdentifier("SCHEMANAME", "TABLENAME01");

        Assert.That(source, Is.Not.SameAs(other));
        EqualityTester.TestEqualObjects(source, other);
    }

    [Test()]
    public void NotEquals_LocalOidCaseInsesitiveTest()
    {
        var source = new DbObjectIdentifier("schemaName", "tableName01");
        var other = new DbObjectIdentifier("SCHEMANAME", "TABLENAME02");

        Assert.That(source, Is.Not.SameAs(other));
        EqualityTester.TestUnequalObjects(source, other);
    }

    [Test()]
    public void Equals_ExternalOidTest1()
    {
        var source = new DbObjectIdentifier(DcmProductivityExtParts, ExampleTableParts);
        var other = new DbObjectIdentifier(DcmProductivityExtParts, ExampleTableParts);

        Assert.That(source, Is.Not.SameAs(other));
        EqualityTester.TestEqualObjects(source, other);
    }

    [Test()]
    public void Equals_ExternalOidCaseInsensitiveTest1()
    {
        var source = new DbObjectIdentifier(DcmProductivityExtParts, new List<string>() { "schemaName", "tableName01" });
        var other = new DbObjectIdentifier(DcmProductivityExtParts, new List<string>() { "SCHEMANAME", "TABLENAME01" });

        Assert.That(source, Is.Not.SameAs(other));
        EqualityTester.TestEqualObjects(source, other);
    }


    [Test()]
    public void NotEquals_ExternalOidCaseInsensitiveTest1()
    {

        var source = new DbObjectIdentifier(DcmProductivityExtParts, new List<string>() { "schemaName", "tableName01" });
        var other = new DbObjectIdentifier(DcmProductivityExtParts, new List<string>() { "SCHEMANAME", "TABLENAME02" });

        Assert.That(source, Is.Not.SameAs(other));

        EqualityTester.TestUnequalObjects(source, other);
    }

    [Test()]
    public void NotEquals_ExternalOidDiffernentExternalCaseInsensitiveTest1()
    {
        var source = new DbObjectIdentifier(DcmProductivityExtParts, new List<string>() { "schemaName", "tableName01" });
        var other = new DbObjectIdentifier(ThiProductivityExtParts, new List<string>() { "schemaName", "tableName01" });

        Assert.That(source, Is.Not.SameAs(other));
        EqualityTester.TestUnequalObjects(source, other);
    }

    [Test()]
    public void GetHashCodeTest()
    {
        Assert.Fail();
    }
}