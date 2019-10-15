using System;
using UIDataBind.Base;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    public abstract class ValueBinder<T> : BaseBinder, IValueBinder<T>
    {
        [SerializeField]
        private T _value;


        public T Value
        {
            get => _value;
            set => UpdateValueHandler(_value = value);
        }

        object IValueBinder.Value
        {
            get => Value;
            set => Value = (T) value;
        }

        public Type ValueType => typeof(T);


        protected override void Bind()
        {
        }

        protected override void Unbind()
        {
        }

        protected override void Dispose()
        {
        }

        protected abstract void UpdateValueHandler(T value);
    }
}