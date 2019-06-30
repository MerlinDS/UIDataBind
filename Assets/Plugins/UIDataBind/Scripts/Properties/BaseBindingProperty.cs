using System;

namespace Plugins.UIDataBind.Properties
{
    public class BaseBindingProperty<TValue> : BaseBindingProperty, IBindingProperty<TValue>
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

        object IBindingProperty.Value
        {
            get => Value;
            set => Value = (TValue) value;
        }

        public BaseBindingProperty(TValue value = default) => Value = value;

        public void Dispose()
        {
        }
    }

    public abstract class BaseBindingProperty
    {
        internal event Action OnInternalUpdateValue;

        protected void InvokeInternalUpdate() => OnInternalUpdateValue?.Invoke();
    }
}