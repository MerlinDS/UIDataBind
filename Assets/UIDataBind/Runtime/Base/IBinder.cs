using System;

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
        OldBindingPath ParentPath { get; }
    }
}