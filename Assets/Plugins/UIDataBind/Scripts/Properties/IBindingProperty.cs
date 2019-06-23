using System;

namespace Plugins.UIDataBind.Properties
{
    public interface IBindingProperty<TValue> : IBindingProperty
    {
        event Action<TValue> OnUpdateValue;
        new TValue Value { get; set; }

    }

    public interface IBindingProperty
    {
        object Value { get; set; }
    }
}