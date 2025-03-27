namespace DevKits.Data.Tests.Core
{
    using Data.Core;

    [TestFixture]
    public class TSQLRosettaTests
    {
        [TestCase("Server.Database.Schema.Object", 1, ExpectedResult = "Object")]
        [TestCase("Server.Database.Schema.Object", 2, ExpectedResult = "Schema")]
        [TestCase("Server.Database.Schema.Object", 3, ExpectedResult = "Database")]
        [TestCase("Server.Database.Schema.Object", 4, ExpectedResult = "Server")]
        [TestCase("Server.Database.Schema.Object", 5, ExpectedResult = null)]
        [TestCase("Object", 1, ExpectedResult = "Object")]
        [TestCase("Object", 2, ExpectedResult = null)]
        public string? ParseName_ShouldReturnCorrectPart(string objectName, int objectPiece)
        {
            return TSQLRosetta.ParseName(objectName, objectPiece);
        }

        [Test]
        public void ParseName_ShouldThrowArgumentNullException_WhenObjectNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => TSQLRosetta.ParseName(null!, 1));
        }

        [TestCase("Server.Database.Schema.Object", "Object", "Schema", "Database", "Server")]
        [TestCase("Database.Schema.Object", "Object", "Schema", "Database", null)]
        [TestCase("Schema.Object", "Object", "Schema", null, null)]
        [TestCase("Object", "Object", "dbo", null, null)]
        public void ParseNameComponents_ShouldReturnCorrectDbSchemaObjectName(string qualifiedSqlObjectName, string expectedObjectName, string expectedSchemaName, string expectedDatabaseName, string expectedServerName)
        {
            var result = TSQLRosetta.ParseNameComponents(qualifiedSqlObjectName);

            Assert.That(result.ObjectName, Is.EqualTo(expectedObjectName));
            Assert.That(result.SchemaName, Is.EqualTo(expectedSchemaName));
            Assert.That(result.DatabaseName, Is.EqualTo(expectedDatabaseName));
            Assert.That(result.ServerName, Is.EqualTo(expectedServerName));
        }

        [TestCase("Object", ExpectedResult = "[Object]")]
        [TestCase("Object", "'", ExpectedResult = "'Object'")]
        [TestCase("Object", "\"", ExpectedResult = "\"Object\"")]
        [TestCase("Object", "()", ExpectedResult = "(Object)")]
        [TestCase("Object", "><", ExpectedResult = ">Object<")]
        [TestCase("Object", "{}", ExpectedResult = "{Object}")]
        [TestCase("Object", "`", ExpectedResult = "`Object`")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = null)]
        public string? QuoteName_ShouldReturnQuotedString(string characterString, string quoteCharacters = "[]")
        {
            return TSQLRosetta.QuoteName(characterString, quoteCharacters);
        }

        [TestCase("[Object]", ExpectedResult = "Object")]
        [TestCase("'Object'", ExpectedResult = "Object")]
        [TestCase("\"Object\"", ExpectedResult = "Object")]
        [TestCase("(Object)", ExpectedResult = "Object")]
        [TestCase(">Object<", ExpectedResult = "Object")]
        [TestCase("{Object}", ExpectedResult = "Object")]
        [TestCase("`Object`", ExpectedResult = "Object")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        public string? RemoveQuotes_ShouldReturnUnquotedString(string characterString)
        {
            return TSQLRosetta.RemoveQuotes(characterString);
        }

        [TestCase("string1", "string1", ExpectedResult = true)]
        [TestCase("string1", "STRING1", ExpectedResult = true)]
        [TestCase("string1", "string2", ExpectedResult = false)]
        [TestCase(null, null, ExpectedResult = true)]
        [TestCase("string1", null, ExpectedResult = false)]
        [TestCase(null, "string1", ExpectedResult = false)]
        public bool AreEqual_ShouldReturnCorrectResult(string source, string other)
        {
            return TSQLRosetta.AreEqual(source, other);
        }

        [TestCase("string1", ExpectedResult = "string1")]
        [TestCase("string1", "string2", ExpectedResult = "string1.string2")]
        [TestCase("string1", "string2", "string3", ExpectedResult = "string1.string2.string3")]
        [TestCase(null, "string2", "string3", ExpectedResult = "string2.string3")]
        [TestCase(null, null, "string3", ExpectedResult = "string3")]
        [TestCase(null, null, null, ExpectedResult = "")]
        public string JoinIfNotEmpty_ShouldReturnConcatenatedString(params string[] stringArgs)
        {
            return TSQLRosetta.JoinIfNotEmpty(".", stringArgs);
        }

        [TestCase(new[] { "string1", "string2" }, new[] { "string1", "string2" }, ExpectedResult = true)]
        [TestCase(new[] { "string1", "string2" }, new[] { "STRING1", "STRING2" }, ExpectedResult = true)]
        [TestCase(new[] { "string1", "string2" }, new[] { "string1", "string3" }, ExpectedResult = false)]
        [TestCase(new[] { "string1", "string2" }, null, ExpectedResult = false)]
        [TestCase(null, new[] { "string1", "string2" }, ExpectedResult = false)]
        [TestCase(null, null, ExpectedResult = true)]
        public bool AreEquals_ShouldReturnCorrectResult(IList<string> source, IList<string> other)
        {
            return TSQLRosetta.AreEquals(source, other);
        }

        [Test]
        [TestCase("string1")]
        [TestCase("STRING1")]
        public void GetHashCode_ShouldReturnCorrectHashCode(string sqlName)
        {
            int expectedHashCode = "string1".GetHashCode(StringComparison.OrdinalIgnoreCase);
            int actualHashCode = TSQLRosetta.GetHashCode(sqlName);
            Assert.That(actualHashCode, Is.EqualTo(expectedHashCode));
        }

        [TestCase("string1", "string1", ExpectedResult = 0)]
        [TestCase("string1", "STRING1", ExpectedResult = 0)]
        [TestCase("string1", "string2", ExpectedResult = -1)]
        [TestCase("string2", "string1", ExpectedResult = 1)]
        [TestCase(null, "string1", ExpectedResult = -1)]
        [TestCase("string1", null, ExpectedResult = 1)]
        [TestCase(null, null, ExpectedResult = 0)]
        public int Compare_ShouldReturnCorrectComparison(string source, string other)
        {
            return TSQLRosetta.Compare(source, other);
        }

        [Test]
        public void CompareSchemaObjectParts_ShouldReturnCorrectComparison()
        {
            Assert.That(TSQLRosetta.CompareSchemaObjectParts("schema1", "object1", "schema1", "object1"), Is.EqualTo(0));
            Assert.That(TSQLRosetta.CompareSchemaObjectParts("schema1", "object1", "schema1", "object2"), Is.EqualTo(-1));
            Assert.That(TSQLRosetta.CompareSchemaObjectParts("schema1", "object2", "schema1", "object1"), Is.EqualTo(1));
            Assert.That(TSQLRosetta.CompareSchemaObjectParts("schema1", "object1", "schema2", "object1"), Is.EqualTo(-1));
            Assert.That(TSQLRosetta.CompareSchemaObjectParts("schema2", "object1", "schema1", "object1"), Is.EqualTo(1));
        }

        [Test]
        public void AreEqual_ISchemaObjectName_ShouldReturnCorrectResult()
        {
            var obj1 = new MockSchemaObjectName("schema1", "object1");
            var obj2 = new MockSchemaObjectName("schema1", "object1");
            var obj3 = new MockSchemaObjectName("schema1", "object2");

            Assert.That(TSQLRosetta.AreEqual(obj1, obj2), Is.True);
            Assert.That(TSQLRosetta.AreEqual(obj1, obj3), Is.False);
            Assert.That(TSQLRosetta.AreEqual(obj1, null), Is.False);
            Assert.That(TSQLRosetta.AreEqual(null, obj2), Is.False);
            Assert.That(TSQLRosetta.AreEqual((ISchemaObjectName?)null, null), Is.True);
        }

        [Test]
        public void AreNotEqual_ISchemaObjectName_ShouldReturnCorrectResult()
        {
            var obj1 = new MockSchemaObjectName("schema1", "object1");
            var obj2 = new MockSchemaObjectName("schema1", "object1");
            var obj3 = new MockSchemaObjectName("schema1", "object2");

            Assert.That(TSQLRosetta.AreNotEqual(obj1, obj2), Is.False);
            Assert.That(TSQLRosetta.AreNotEqual(obj1, obj3), Is.True);
            Assert.That(TSQLRosetta.AreNotEqual(obj1, null), Is.True);
            Assert.That(TSQLRosetta.AreNotEqual(null, obj2), Is.True);
            Assert.That(TSQLRosetta.AreNotEqual(null, null), Is.False);
        }

        [Test]
        public void Compare_ISchemaObjectName_ShouldReturnCorrectComparison()
        {
            var obj1 = new MockSchemaObjectName("schema1", "object1");
            var obj2 = new MockSchemaObjectName("schema1", "object1");
            var obj3 = new MockSchemaObjectName("schema1", "object2");
            var obj4 = new MockSchemaObjectName("schema2", "object1");

            Assert.That(TSQLRosetta.Compare(obj1, obj2), Is.EqualTo(0));
            Assert.That(TSQLRosetta.Compare(obj1, obj3), Is.LessThan(0));
            Assert.That(TSQLRosetta.Compare(obj3, obj1), Is.GreaterThan(0));
            Assert.That(TSQLRosetta.Compare(obj1, obj4), Is.LessThan(0));
            Assert.That(TSQLRosetta.Compare(obj4, obj1), Is.GreaterThan(0));
            Assert.That(TSQLRosetta.Compare(obj1, null), Is.GreaterThan(0));
            Assert.That(TSQLRosetta.Compare(null, obj2), Is.LessThan(0));
            Assert.That(TSQLRosetta.Compare((ISchemaObjectName?)null, null), Is.EqualTo(0));
        }

        private class MockSchemaObjectName : ISchemaObjectName
        {
            public MockSchemaObjectName(string schemaName, string objectName)
            {
                SchemaName = schemaName;
                ObjectName = objectName;
            }

            public string SchemaName { get; }
            public string ObjectName { get; }

            public string ToQuotedSqlString() => $"{SchemaName}.{ObjectName}";

            public bool Equals(ISchemaObjectName? other)
            {
                return TSQLRosetta.AreEqual(this, other);
            }

            public int CompareTo(ISchemaObjectName? other)
            {
                return TSQLRosetta.Compare(this, other);
            }
        }
    }
}
