namespace DevKits.OddsAndEnds.Core.Tests.Collections
{
    using Core.Collections;

    public class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class TestKeyedCollection : KeyedCollectionPlus<int, TestItem>
    {
        protected override int GetKeyForItem(TestItem item)
        {
            return item.Id;
        }
    }

    [TestFixture]
    public class KeyedCollectionPlusTests
    {
        [SetUp]
        public void SetUp()
        {
            _collection = new TestKeyedCollection();
        }

        private TestKeyedCollection _collection;

        [Test]
        public void TryAdd_AddsItemIfNotExists()
        {
            var item = new TestItem { Id = 1, Name = "Item1" };
            Assert.That(_collection.TryAdd(item), Is.True);
            Assert.That(_collection.Contains(item), Is.True);
        }

        [Test]
        public void TryAdd_DoesNotAddItemIfExists()
        {
            var item = new TestItem { Id = 1, Name = "Item1" };
            _collection.Add(item);
            var newItem = new TestItem { Id = 1, Name = "NewItem" };
            Assert.That(_collection.TryAdd(newItem), Is.False);
            Assert.That(_collection.Contains(newItem), Is.False);
        }

        [Test]
        public void AddRange_AddsMultipleItems()
        {
            var items = new List<TestItem>
            {
                new() { Id = 1, Name = "Item1" },
                new() { Id = 2, Name = "Item2" }
            };
            _collection.AddRange(items);
            Assert.That(_collection.Contains(items[0]), Is.True);
            Assert.That(_collection.Contains(items[1]), Is.True);
        }

        [Test]
        public void AddRange_DoesNotAddDuplicateItems()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var items = new List<TestItem>
                {
                    new() { Id = 1, Name = "Item1" },
                    new() { Id = 2, Name = "Item2" }
                };
                _collection.AddRange(items);
                var newItems = new List<TestItem>
                {
                    new() { Id = 1, Name = "NewItem1" },
                    new() { Id = 3, Name = "Item3" }
                };
                _collection.AddRange(newItems);
            });
        }

        [Test]
        public void TryAddRange_DoesNotAddDuplicateItems()
        {
            var items = new List<TestItem>
            {
                new() { Id = 1, Name = "Item1" },
                new() { Id = 2, Name = "Item2" }
            };
            _collection.TryAddRange(items);
            var newItems = new List<TestItem>
            {
                new() { Id = 1, Name = "NewItem1" },
                new() { Id = 3, Name = "Item3" }
            };
            _collection.TryAddRange(newItems);
            Assert.That(_collection.Contains(newItems[0]), Is.False);
            Assert.That(_collection.Contains(newItems[1]), Is.True);
        }

        [Test]
        public void MultipleAddsOfSameKey_DoesNotAddDuplicateItems()
        {
            var items = new List<TestItem>
            {
                new() { Id = 1, Name = "Item1" },
                new() { Id = 2, Name = "Item2" }
            };
            foreach (var testItem in items)
            {
                _collection.Add(testItem);
            }

            var newItems = new List<TestItem>
            {
                new() { Id = 1, Name = "NewItem1" },
                new() { Id = 3, Name = "Item3" }
            };

            Assert.Throws<ArgumentException>(() =>
                {
                    foreach (var testItem in newItems)
                    {
                        _collection.Add(testItem);
                    }
                }
            );

        }


        [Test]
        public void Contains_ReturnsTrueIfItemExists()
        {
            var item = new TestItem { Id = 1, Name = "Item1" };
            _collection.Add(item);
            Assert.That(_collection.Contains(item), Is.True);
        }

        [Test]
        public void Contains_ReturnsFalseIfItemDoesNotExist()
        {
            var item = new TestItem { Id = 1, Name = "Item1" };
            Assert.That(_collection.Contains(item), Is.False);
        }

        [Test]
        public void ContainsKey_ReturnsTrueIfKeyExists()
        {
            var item = new TestItem { Id = 1, Name = "Item1" };
            _collection.Add(item);
            Assert.That(_collection.Contains(1), Is.True);
        }

        [Test]
        public void ContainsKey_ReturnsFalseIfKeyDoesNotExist()
        {
            Assert.That(_collection.Contains(1), Is.False);
        }

        [Test]
        public void TryGetValue_ReturnsTrueIfKeyExists()
        {
            var item = new TestItem { Id = 1, Name = "Item1" };
            _collection.Add(item);
            Assert.That(_collection.TryGetValue(1, out var retrievedItem), Is.True);
            Assert.That(retrievedItem, Is.EqualTo(item));
        }

        [Test]
        public void TryGetValue_ReturnsFalseIfKeyDoesNotExist()
        {
            Assert.That(_collection.TryGetValue(1, out _), Is.False);
        }
    }
}