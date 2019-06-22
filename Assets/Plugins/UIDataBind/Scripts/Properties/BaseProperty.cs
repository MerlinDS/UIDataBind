namespace Plugins.UIDataBind.Properties
{
    public abstract class BaseProperty<TValue>
    {
        private TValue _value;

        public delegate void UpdateHandler(TValue value);
        public event UpdateHandler UpdateValue;

        protected BaseProperty(TValue value) => _value = value;

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                UpdateValue?.Invoke(value);
            }
        }
    }
}