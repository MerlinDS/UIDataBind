using System;
using UIDataBindCore.Properties.Adapters;

namespace UIDataBindCore.Extensions
{
    public static class BindPropertyConvertingExtension
    {
        public static IBindProperty<TValue> AsPropertyOf<TValue>(this IBindProperty source)
        {
            switch (source)
            {
                case IBindProperty<byte> property:
                    return property.To<TValue>();
                case IBindProperty<int> property:
                    return property.To<TValue>();
                case IBindProperty<float> property:
                    return property.To<TValue>();
                case IBindProperty<double> property:
                    return property.To<TValue>();
                case IBindProperty<decimal> property:
                    return property.To<TValue>();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}