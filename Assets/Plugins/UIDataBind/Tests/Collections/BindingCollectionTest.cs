using NUnit.Framework;
using Plugins.UIDataBind.Collections;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Collections
{
    public class BindingCollectionTest
    {
        private BindingCollection<int> _collection;

        [SetUp]
        public void SetUp()
        {
            var array = new int[Random.Range(0, 10)];
            for (var i = 0; i < array.Length; i++)
                array[i] = i + 1;

            _collection = new BindingCollection<int>(array);
        }

        [TearDown]
        public void TearDown()
        {
            _collection.Dispose();
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
            var expectedCount = _collection.Count - 1;
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
            Assert.AreEqual(expectedCount, _collection.Count);
        }
    }
}