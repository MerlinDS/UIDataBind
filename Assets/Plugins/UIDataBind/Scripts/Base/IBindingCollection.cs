using System;
using System.Collections.Generic;

namespace Plugins.UIDataBind.Base
{
    public interface IBindingCollection<TValue> : ICollection<TValue>, IDisposable
    {
        #region Events

        event Action<int, TValue> OnItemAdded;
        event Action<int, TValue> OnItemRemoved;

        event Action<int, TValue, TValue> OnItemChanged;

        event Action OnClear;

        #endregion

        #region API

        TValue this[int index] { get; set; }
        void Insert(int index, TValue value);

        void RemoveAt(int index);

        #endregion
    }
}