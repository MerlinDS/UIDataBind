using System;
using Plugins.UIDataBind.Base;

namespace Plugins.UIDataBind.Properties
{
    public class BindingProperty<TValue> : BindingProperty, IBindingProperty<TValue>
    {
        private TValue _value;
        public event Action<TValue> OnUpdateValue;

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                OnUpdateValue?.Invoke(value);
                InvokeInternalUpdate();
            }
        }

        public Type GetValueType => typeof(TValue);

        object IBindingProperty.Value
        {
            get => Value;
            set => Value = (TValue) value;
        }

        public BindingProperty(TValue value = default) => Value = value;

        public void Dispose()
        {
        }
    }

    public abstract class BindingProperty
    {
        internal event Action OnInternalUpdateValue;

        protected void InvokeInternalUpdate() => OnInternalUpdateValue?.Invoke();
    }
}