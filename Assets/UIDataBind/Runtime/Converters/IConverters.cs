using System;

namespace UIDataBind.Converters
{
    public interface IConverters
    {
        void     Register<TSource, TTarget>(Func<TSource, TTarget> method);
        TTarget Convert<TTarget>(object value);
        object Convert<TSource>(Type targetType, TSource value);

        bool Has(Type a, Type b);
    }
}