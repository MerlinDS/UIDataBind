using System;
using Plugins.UIDataBind.Extensions;

namespace Plugins.UIDataBind.Properties
{
    public sealed class PropertyAdapter<TValue> : IBindingProperty<TValue>
    {
        private readonly IBindingProperty _source;
        public event Action<TValue> OnUpdateValue;


        object IBindingProperty.Value
        {
            get => _source.Value;
            set => _source.Value = value;
        }


        public TValue Value
        {
            get => (TValue)_source.ConvertTo<TValue>();
            set
            {
                _source.Value = value;
                OnUpdateValue?.Invoke(value);
            }
        }

        public PropertyAdapter(IBindingProperty source) => _source = source;
    }
}