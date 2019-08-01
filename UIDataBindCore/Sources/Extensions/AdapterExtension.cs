using System;
using UIDataBindCore.Base;
using UIDataBindCore.Converters;

namespace UIDataBindCore.Extensions
{
    public static partial class AdapterExtension
    {

        public static IBindProperty<TValue> AsPropertyOf<TValue>(this IBindProperty source)
        {
            switch (source)
            {
                case IBindProperty<bool> property:
                    return property.To<TValue>();
                case IBindProperty<byte> property:
                    return property.To<TValue>();
                case IBindProperty<int> property:
                    return property.To<TValue>();
                case IBindProperty<float> property:
                    return property.To<TValue>();
                case IBindProperty<double> property:
                    return property.To<TValue>();
                case IBindProperty<string> property:
                    return property.To<TValue>();
                default:
                    throw new NotImplementedException();
            }
        }

        public static IBindProperty<TValue> NewAsPropertyOf<TValue>(this IBindProperty source)
        {

            var collection = new ConvertersCollection();
            var converter = collection.Retrieve<TValue>(source.GetType());
            if (converter != null)
            {

                //TODO: Create Adapter with converter
            }

            throw new NotImplementedException();
        }
    }
}