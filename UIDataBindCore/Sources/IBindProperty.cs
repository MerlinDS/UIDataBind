using System;

namespace UIDataBindCore
{
    public interface IBindProperty<TValue> : IBindProperty
    {
        event Action<TValue> OnUpdate;
        TValue Value { get; set; }
    }

    public interface IBindProperty : IDisposable
    {
        Type ValueType { get;}
    }
}