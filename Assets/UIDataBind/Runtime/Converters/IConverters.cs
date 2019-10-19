using System;

namespace UIDataBind.Converters
{
    public interface IConverters
    {
        void     Register<TSource, TTarget>(Func<TSource, TTarget> method);
        TTarget Convert<TTarget>(object sourceValue);
        object Convert<TSource>(Type targetType, TSource sourceValue);
        Delegate Retrieve(Type a, Type b);


        bool Has(Type a, Type b);
    }
}