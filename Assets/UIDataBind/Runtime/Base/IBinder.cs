using System;
using UIDataBind.Binders;
using UIDataBind.Binders.Attributes;

namespace UIDataBind.Base
{
    public interface IValueBinder<T> : IValueBinder
    {
        new T Value { get; set; }
    }

    public interface IValueBinder : IBinder
    {
        Type ValueType { get; }
        object Value { get; set; }
    }
    public interface IBinder : IBindingPathProvider, IEngineProvider
    {
        BindingType BindingType { get; }
    }
}