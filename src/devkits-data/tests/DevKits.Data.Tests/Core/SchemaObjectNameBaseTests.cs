namespace DevKits.Data.Tests.Core
{
    using Data.Core;

    using NUnit.Framework;
    using TestTools;

    [TestFixture]
    public class SchemaObjectNameBaseTests
    {
        [Test]
        public void ToString_ShouldReturnFullyQualifiedName()
        {
            // Arrange
            string schemaName = "dbo";
            string objectName = "MyTable";
            var schemaObjectName = new TestSchemaObjectName(schemaName, objectName);

            // Act
            string result = schemaObjectName.ToString();

            // Assert
            Assert.That(result, Is.EqualTo("dbo.MyTable"));
        }

        [Test]
        public void ToQuotedSqlString_ShouldReturnQuotedFullyQualifiedName()
        {
            // Arrange
            string schemaName = "dbo";
            string objectName = "MyTable";
            var schemaObjectName = new TestSchemaObjectName(schemaName, objectName);

            // Act
            string result = schemaObjectName.ToQuotedSqlString();

            // Assert
            Assert.That(result, Is.EqualTo("[dbo].[MyTable]"));
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenObjectsAreEqual()
        {
            // Arrange
            string schemaName = "dbo";
            string objectName = "MyTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName, objectName);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName, objectName);

            // Act
            bool result = schemaObjectName1.Equals(schemaObjectName2);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Equality_ShouldBeImplementedCompletely()
        {
            // Arrange
            string schemaName = "dbo";
            string objectName = "MyTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName, objectName);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName, objectName);
            Assert.That(schemaObjectName1, Is.Not.Null);
            Assert.That(schemaObjectName2, Is.Not.Null);

            EqualityTester.TestEqualObjects(schemaObjectName1, schemaObjectName2);
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenObjectsAreNotEqual()
        {
            // Arrange
            string schemaName1 = "dbo";
            string objectName1 = "MyTable";
            string schemaName2 = "dbo";
            string objectName2 = "OtherTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName1, objectName1);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName2, objectName2);

            // Act
            bool result = schemaObjectName1.Equals(schemaObjectName2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetHashCode_ShouldReturnSameValue_WhenObjectsAreEqual()
        {
            // Arrange
            string schemaName = "dbo";
            string objectName = "MyTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName, objectName);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName, objectName);

            // Act
            int hashCode1 = schemaObjectName1.GetHashCode();
            int hashCode2 = schemaObjectName2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.EqualTo(hashCode2));
        }

        [Test]
        public void GetHashCode_ShouldReturnDifferentValue_WhenObjectsAreNotEqual()
        {
            // Arrange
            string schemaName1 = "dbo";
            string objectName1 = "MyTable";
            string schemaName2 = "dbo";
            string objectName2 = "OtherTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName1, objectName1);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName2, objectName2);

            // Act
            int hashCode1 = schemaObjectName1.GetHashCode();
            int hashCode2 = schemaObjectName2.GetHashCode();

            // Assert
            Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
        }

        [Test]
        public void CompareTo_ShouldReturnNegativeValue_WhenCurrentObjectPrecedesOtherObject()
        {
            // Arrange
            string schemaName1 = "dbo";
            string objectName1 = "MyTable";
            string schemaName2 = "dbo";
            string objectName2 = "OtherTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName1, objectName1);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName2, objectName2);

            // Act
            int result = schemaObjectName1.CompareTo(schemaObjectName2);

            // Assert
            Assert.Less(result, 0);
        }

        [Test]
        public void CompareTo_ShouldReturnPositiveValue_WhenCurrentObjectFollowsOtherObject()
        {
            // Arrange
            string schemaName1 = "dbo";
            string objectName1 = "OtherTable";
            string schemaName2 = "dbo";
            string objectName2 = "MyTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName1, objectName1);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName2, objectName2);

            // Act
            int result = schemaObjectName1.CompareTo(schemaObjectName2);

            // Assert
            Assert.Greater(result, 0);
        }

        [Test]
        public void CompareTo_ShouldReturnZero_WhenCurrentObjectIsSameAsOtherObject()
        {
            // Arrange
            string schemaName = "dbo";
            string objectName = "MyTable";
            var schemaObjectName1 = new TestSchemaObjectName(schemaName, objectName);
            var schemaObjectName2 = new TestSchemaObjectName(schemaName, objectName);

            // Act
            int result = schemaObjectName1.CompareTo(schemaObjectName2);

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        private class TestSchemaObjectName : SchemaObjectNameBase, IEquatable<TestSchemaObjectName>
        {
            public TestSchemaObjectName(string? schemaName, string objectName)
                : base(schemaName, objectName)
            {
            }

            #region Implementation of IEquatable<TestSchemaObjectName>

            /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
            /// <param name="other">An object to compare with this object.</param>
            /// <returns>
            /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
            public bool Equals(TestSchemaObjectName? other)
            {
               return  base.Equals(other);
            }

            /// <summary>Determines whether the specified object is equal to the current object.</summary>
            /// <param name="obj">The object to compare with the current object.</param>
            /// <returns>
            ///     <see langword="true" /> if the specified object  is equal to the current object; otherwise,
            ///     <see langword="false" />.
            /// </returns>
            public override bool Equals(object? obj)
            {
                return TSQLRosetta.AreNotEqual(this, (ISchemaObjectName?) obj);
            }

            public static bool operator ==(TestSchemaObjectName? left, TestSchemaObjectName? right)
            {
                return TSQLRosetta.AreEqual(left, (ISchemaObjectName?)right);
            }

            /// <summary>
            ///     Returns a value that indicates whether two <see cref="T:DevKits.CodeGen.QualifiedSQLObjectName" /> objects
            ///     have different values.
            /// </summary>
            /// <param name="left">The first value to compare.</param>
            /// <param name="right">The second value to compare.</param>
            /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
            public static bool operator !=(TestSchemaObjectName? left, TestSchemaObjectName? right)
            {
                return TSQLRosetta.AreNotEqual(left, right);
            }


            /// <summary>Serves as the default hash function.</summary>
            /// <returns>A hash code for the current object.</returns>
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            #endregion



        }
    }
}
