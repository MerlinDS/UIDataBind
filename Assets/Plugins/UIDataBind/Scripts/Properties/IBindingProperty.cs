using System;

namespace Plugins.UIDataBind.Properties
{
    public interface IBindingProperty<TValue> : IBindingProperty
    {
        event Action<TValue> OnUpdateValue;
        TValue Value { get; set; }

        void SilentSet(TValue value);
    }

    public interface IBindingProperty
    {

    }
}