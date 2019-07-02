using NUnit.Framework;
using Plugins.UIDataBind.Components;
using UnityEngine;

namespace Plugins.UIDataBind.Tests.Components
{
    public class CollectionItemViewContextTest
    {
        private TestCollectionItemView _itemView;

        [SetUp]
        public void SetUp()
        {
            var testObject = new GameObject("TestGameObject");
            _itemView = testObject.AddComponent<TestCollectionItemView>();
        }

        [TearDown]
        public void TearDown() =>
            Object.DestroyImmediate(_itemView.gameObject);


        [Test]
        public void ConfigureTest()
        {
            var expectedValue = Random.value;
            _itemView.Configure(expectedValue);
            Assert.AreEqual(_itemView.Data, expectedValue);
        }

        [Test]
        public void ConfigureWithInvalidCastTest()
        {
            //Disable log during this test: The legal Debug.LogError will fail this test
            var logEnabled = Debug.unityLogger.logEnabled;
            Debug.unityLogger.logEnabled = false;
            Assert.DoesNotThrow(()=>_itemView.Configure("Incorrect data value"));
            Debug.unityLogger.logEnabled = logEnabled;
        }

    }

    #region Design Area

    public class TestCollectionItemView : CollectionItemViewContext<float>
    {
        public float Data { get; set; }
        protected override void Configure(float data) => Data = data;
    }

    #endregion
}