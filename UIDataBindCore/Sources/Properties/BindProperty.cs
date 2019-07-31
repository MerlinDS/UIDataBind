using System;

namespace UIDataBindCore.Properties
{
    public class BindProperty<TValue> : IBindProperty<TValue>
    {
        private TValue _value;

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                OnUpdate?.Invoke(value);
            }
        }

        public Type ValueType => typeof(TValue);

        public BindProperty(TValue value = default) => Value = value;
        public virtual void Dispose(){}

        public event Action<TValue> OnUpdate;
    }
}