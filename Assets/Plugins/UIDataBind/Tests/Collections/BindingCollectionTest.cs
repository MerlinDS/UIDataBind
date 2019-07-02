using System.Linq;
using NUnit.Framework;
using Plugins.UIDataBind.Collections;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Collections
{
    public class BindingCollectionTest
    {
        private int _baseCount;
        private int[] _baseCollection;

        private BindingCollection<int> _collection;

        [SetUp]
        public void SetUp()
        {
            _baseCount = Random.Range(10, 20);
            _baseCollection = new int[_baseCount];
            for (var i = 0; i < _baseCollection.Length; i++)
                _baseCollection[i] = i + 1;

            _collection = new BindingCollection<int>(_baseCollection);
        }

        [TearDown]
        public void TearDown() => _collection.Dispose();

        [Test]
        public void CountTest() =>
            Assert.AreEqual(_baseCount, _collection.Count);

        [Test]
        public void IsReadOnlyTest() =>
            Assert.IsFalse(_collection.IsReadOnly);

        [Test]
        public void GetEnumeratorTest()
        {
            var enumerator = _collection.GetEnumerator();
            Assert.NotNull(enumerator);
            enumerator.Reset();

            while (enumerator.MoveNext())
                Assert.IsTrue(_baseCollection.Contains(enumerator.Current));
            enumerator.Dispose();
        }

        [Test]
        public void ContainsTest()
        {
            Assert.IsTrue(_collection.Contains(Random.Range(1, _baseCount)));
            Assert.IsFalse(_collection.Contains(int.MinValue));
        }

        [Test]
        public void CopyToTest()
        {
            var array = new int[_baseCount];
            _collection.CopyTo(array, 0);
            for (var i = 0; i < array.Length; i++)
                Assert.AreEqual(_baseCollection[i], array[i]);
        }


        [Test]
        public void AddingTest()
        {
            var expectedItem = Random.Range(0, 1000);
            var index = _collection.Count;
            var eventOccurs = false;

            void Handler(int i, int item)
            {
                Assert.AreEqual(index, i);
                Assert.AreEqual(expectedItem, item);
                eventOccurs = true;
            }

            _collection.OnItemAdded += Handler;
            _collection.Add(expectedItem);
            _collection.OnItemAdded -= Handler;

            Assert.IsTrue(eventOccurs);
            Assert.AreEqual(index + 1, _collection.Count);
            Assert.AreEqual(expectedItem, _collection[index]);
        }

        [Test]
        public void RemovingTest()
        {
            var index = Random.Range(0, _collection.Count - 1);
            var expectedItem = _collection[index];
            var eventOccurs = false;

            void Handler(int i, int item)
            {
                Assert.AreEqual(index, i);
                Assert.AreEqual(expectedItem, item);
                eventOccurs = true;
            }

            _collection.OnItemRemoved += Handler;
            _collection.Remove(expectedItem);
            _collection.OnItemRemoved -= Handler;

            Assert.IsTrue(eventOccurs);
            Assert.AreEqual(_baseCount - 1, _collection.Count);
        }

        [Test]
        public void IndexerTest()
        {
            var index = Random.Range(0, _collection.Count - 1);
            Assert.AreEqual(_baseCollection[index], _collection[index]);
            var expectedItem = Random.Range(_baseCount, int.MinValue);
            var eventOccurs = false;

            void Handler(int i, int oldItem, int newItem)
            {
                Assert.AreEqual(index, i);
                Assert.AreEqual(_baseCollection[index], oldItem);
                Assert.AreEqual(expectedItem, newItem);
                eventOccurs = true;
            }

            _collection.OnItemChanged += Handler;
            _collection[index] = expectedItem;
            _collection.OnItemChanged -= Handler;

            Assert.AreEqual(expectedItem, _collection[index]);
            Assert.AreEqual(_baseCount, _collection.Count);
            Assert.IsTrue(eventOccurs);
        }

        [Test]
        public void InsertTest()
        {
            var index = Random.Range(0, _collection.Count - 1);
            var expectedItem = Random.Range(_baseCount, int.MinValue);
            var eventOccurs = false;

            void Handler(int i, int item)
            {
                Assert.AreEqual(index, i);
                Assert.AreEqual(expectedItem, item);
                eventOccurs = true;
            }

            _collection.OnItemAdded += Handler;
            _collection.Insert(index, expectedItem);
            _collection.OnItemAdded -= Handler;

            Assert.AreEqual(expectedItem, _collection[index]);
            Assert.AreEqual(_baseCount + 1, _collection.Count);
            Assert.IsTrue(eventOccurs);
        }

        [Test]
        public void RemoveAtTest()
        {
            var index = Random.Range(0, _collection.Count - 1);
            var expectedItem = _collection[index];
            var eventOccurs = false;

            void Handler(int i, int item)
            {
                Assert.AreEqual(index, i);
                Assert.AreEqual(expectedItem, item);
                eventOccurs = true;
            }

            _collection.OnItemRemoved += Handler;
            _collection.RemoveAt(index);
            _collection.OnItemRemoved -= Handler;

            Assert.IsTrue(eventOccurs);
            Assert.AreEqual(_baseCount - 1, _collection.Count);
        }

        [Test]
        public void ClearTest()
        {
            var eventOccurs = false;
            void Handler() => eventOccurs = true;

            _collection.OnClear += Handler;
            _collection.Clear();
            _collection.OnClear -= Handler;

            Assert.IsTrue(eventOccurs);
            Assert.AreEqual(0, _collection.Count);
        }

        [Test]
        public void RemovedAllTest()
        {
            var eventOccurs = false;
            void Handler()
            {
                Assert.IsFalse(eventOccurs);
                eventOccurs = true;
            }

            _collection.OnClear += Handler;

            while (_collection.Count > 0)
                _collection.RemoveAt(0);

            _collection.OnClear -= Handler;

            Assert.IsTrue(eventOccurs);
        }
    }
}