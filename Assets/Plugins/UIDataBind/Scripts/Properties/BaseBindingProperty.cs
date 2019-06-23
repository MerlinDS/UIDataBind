using System;

namespace Plugins.UIDataBind.Properties
{
    public class BaseBindingProperty<TValue> : IBindingProperty<TValue>
    {
        private TValue _value;
        public event Action<TValue> OnUpdateValue;

        object IBindingProperty.Value
        {
            get => Value;
            set => Value = (TValue) value;
        }

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                OnUpdateValue?.Invoke(value);
            }
        }

        public BaseBindingProperty(TValue value = default) => Value = value;
    }
}