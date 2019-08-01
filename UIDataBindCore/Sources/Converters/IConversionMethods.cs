using System;

namespace UIDataBindCore.Converters
{
    public interface IConversionMethods
    {
        void Register<TType0, TType1>(Func<TType1, TType0> from1To0, Func<TType0, TType1> from0To1);
        Delegate Retrieve(Type type0, Type type1);

        bool Has(Type type0, Type type1);
        void Dispose();
    }
}