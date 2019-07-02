using System;
using System.Collections;
using System.Collections.Generic;
using Plugins.UIDataBind.Base;

namespace Plugins.UIDataBind.Collections
{
    public class BindingCollection<TValue> : IBindingCollection<TValue>
    {
        #region Events

        public event Action<int, TValue> OnItemAdded;
        public event Action<int, TValue> OnItemRemoved;

        public event Action<int, TValue, TValue> OnItemChanged;

        public event Action OnClear;

        #endregion

        private readonly IList<TValue> _internalCollection = new List<TValue>();

        #region Properties

        public int Count => _internalCollection.Count;

        public bool IsReadOnly => _internalCollection.IsReadOnly;

        #endregion

        #region Collection API

        public IEnumerator<TValue> GetEnumerator() =>
            _internalCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public bool Contains(TValue item) =>
            _internalCollection.Contains(item);
        public void CopyTo(TValue[] array, int arrayIndex) =>
            _internalCollection.CopyTo(array, arrayIndex);


        public TValue this[int index]
        {
            get => _internalCollection[index];
            set
            {
                var oldValue = _internalCollection[index];
                _internalCollection[index] = value;
                OnItemChanged?.Invoke(index, oldValue, value);
            }
        }

        public void Add(TValue item)
        {
            _internalCollection.Add(item);
            OnItemAdded?.Invoke(_internalCollection.Count - 1, item);
        }

        public void Insert(int index, TValue value)
        {
            _internalCollection.Insert(index, value);
            OnItemAdded?.Invoke(index, value);
            //OnItemChanged?.Invoke(index, oldValue, value);
        }
        public void Clear()
        {
            _internalCollection.Clear();
            OnClear?.Invoke();
        }


        public bool Remove(TValue item)
        {
            var index = _internalCollection.IndexOf(item);
            var result = _internalCollection.Remove(item);
            if(result)
                ItemRemoved(index, item);
            return result;
        }

        public void RemoveAt(int index)
        {
            var item = this[index];
            _internalCollection.RemoveAt(index);
            ItemRemoved(index, item);
        }

        #endregion

        public void Dispose() =>
            _internalCollection.Clear();

        private void ItemRemoved(int index, TValue item)
        {
            OnItemRemoved?.Invoke(index, item);
            if(_internalCollection.Count == 0)
                OnClear?.Invoke();
        }

    }
}