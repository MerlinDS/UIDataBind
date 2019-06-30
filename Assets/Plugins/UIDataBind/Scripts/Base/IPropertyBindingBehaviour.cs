using System;

namespace Plugins.UIDataBind.Base
{
    public interface IPropertyBindingBehaviour<TValue> : IPropertyBindingBehaviour
    {
        TValue Value { get; set; }
    }

    public interface IPropertyBindingBehaviour
    {
        Type GetValueType { get; }
    }
}