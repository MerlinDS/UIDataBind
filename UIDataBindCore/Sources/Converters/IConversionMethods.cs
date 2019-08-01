using System;

namespace UIDataBindCore.Converters
{
    public interface IConversionMethods
    {
        void Register<TType0, TType1>(Func<TType0, TType1> from0To1, Func<TType1, TType0> from1To0);
        Func<TType0, TType1> Retrieve<TType0, TType1>();
        void Dispose();
    }
}