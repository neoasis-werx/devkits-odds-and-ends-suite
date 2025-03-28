#pragma warning disable S1481
namespace DevKits.OddsAndEnds.Core.Tests.Collections
{
    using Core.Collections;

    [TestFixture]
    public class BiDirectionalDictionaryTests
    {
        private BiDirectionalDictionary<string, int> _dictionary;

        [SetUp]
        public void SetUp()
        {
            _dictionary = new BiDirectionalDictionary<string, int>();
        }

        [Test]
        public void Add_AddsKeyValuePair()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.Forward("one"), Is.EqualTo(1));
            Assert.That(_dictionary.Reverse(1), Is.EqualTo("one"));
        }

        [Test]
        public void Add_ThrowsExceptionOnDuplicateKeyOrValue()
        {
            _dictionary.Add("one", 1);
            Assert.That(() => _dictionary.Add("one", 2), Throws.ArgumentException);
            Assert.That(() => _dictionary.Add("two", 1), Throws.ArgumentException);
        }

        [Test]
        public void RemoveByKey_RemovesKeyValuePair()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.RemoveByKey("one"), Is.True);
            Assert.That(_dictionary.ContainsForward("one"), Is.False);
            Assert.That(_dictionary.ContainsReverse(1), Is.False);
        }

        [Test]
        public void RemoveByValue_RemovesKeyValuePair()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.RemoveByValue(1), Is.True);
            Assert.That(_dictionary.ContainsForward("one"), Is.False);
            Assert.That(_dictionary.ContainsReverse(1), Is.False);
        }

        [Test]
        public void TryForward_ReturnsTrueIfKeyExists()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.TryForward("one", out var value), Is.True);
            Assert.That(value, Is.EqualTo(1));
        }

        [Test]
        public void TryForward_ReturnsFalseIfKeyDoesNotExist()
        {
            Assert.That(_dictionary.TryForward("one", out var _), Is.False);
        }

        [Test]
        public void TryReverse_ReturnsTrueIfValueExists()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.TryReverse(1, out var key), Is.True);
            Assert.That(key, Is.EqualTo("one"));
        }

        [Test]
        public void TryReverse_ReturnsFalseIfValueDoesNotExist()
        {
            Assert.That(_dictionary.TryReverse(1, out var _), Is.False);
        }

        [Test]
        public void ContainsForward_ReturnsTrueIfKeyExists()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.ContainsForward("one"), Is.True);
        }

        [Test]
        public void ContainsForward_ReturnsFalseIfKeyDoesNotExist()
        {
            Assert.That(_dictionary.ContainsForward("one"), Is.False);
        }

        [Test]
        public void ContainsReverse_ReturnsTrueIfValueExists()
        {
            _dictionary.Add("one", 1);
            Assert.That(_dictionary.ContainsReverse(1), Is.True);
        }

        [Test]
        public void ContainsReverse_ReturnsFalseIfValueDoesNotExist()
        {
            Assert.That(_dictionary.ContainsReverse(1), Is.False);
        }

        [Test]
        public void Count_ReturnsNumberOfMappings()
        {
            _dictionary.Add("one", 1);
            _dictionary.Add("two", 2);
            Assert.That(_dictionary.Count, Is.EqualTo(2));
        }

        [Test]
        public void ToJson_SerializesDictionaryToJson()
        {
            _dictionary.Add("one", 1);
            var json = _dictionary.ToJson();
            Assert.That(json, Is.EqualTo("{\"one\":1}"));
        }


        [Test]
        public void FromJson_DeserializesDictionaryFromJson()
        {
            var json = "{\"one\":1}";
            var dictionary = BiDirectionalDictionary<string, int>.FromJson(json);
            Assert.That(dictionary.Forward("one"), Is.EqualTo(1));
            Assert.That(dictionary.Reverse(1), Is.EqualTo("one"));
        }

        [Test]
        public void AddRange_AddsMultipleKeyValuePairs()
        {
            var items = new Dictionary<string, int> { { "one", 1 }, { "two", 2 } };
            _dictionary.AddRange(items);
            Assert.That(_dictionary.Forward("one"), Is.EqualTo(1));
            Assert.That(_dictionary.Forward("two"), Is.EqualTo(2));
        }

        [Test]
        public void RemoveByKeys_RemovesMultipleKeys()
        {
            _dictionary.Add("one", 1);
            _dictionary.Add("two", 2);
            _dictionary.RemoveByKeys(new[] { "one", "two" });
            Assert.That(_dictionary.ContainsForward("one"), Is.False);
            Assert.That(_dictionary.ContainsForward("two"), Is.False);
        }
    }
}
