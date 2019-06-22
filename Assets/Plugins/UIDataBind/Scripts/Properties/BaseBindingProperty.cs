using System;

namespace Plugins.UIDataBind.Properties
{
    public class BaseBindingProperty<TValue> : IBindingProperty<TValue>
    {
        private TValue _value;

        public event Action<TValue> OnUpdateValue;

        public BaseBindingProperty(TValue value = default) => Value = value;

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                OnUpdateValue?.Invoke(value);
            }
        }

        public void SilentSet(TValue value) => _value = value;
    }
}