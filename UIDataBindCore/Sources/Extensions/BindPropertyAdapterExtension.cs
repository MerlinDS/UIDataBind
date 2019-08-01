using System;
using UIDataBindCore.Converters;
using UIDataBindCore.Properties;

namespace UIDataBindCore.Extensions
{
    public static class BindPropertyAdapterExtension
    {
        private static readonly Type BindPropertyAdapterType = typeof(BindPropertyAdapter<,>);

        public static IBindProperty<TValue> AsPropertyOf<TValue>(this ConvertersCollection converters, IBindProperty source)
        {
            if (converters == null)
                throw new ArgumentNullException(nameof(converters));

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var targetType = typeof(TValue);
            if (targetType == source.ValueType)
                return (IBindProperty<TValue>)source;

            var converter = converters.Retrieve<TValue>(source.ValueType);
            if (converter == null)
                return null;

            return (IBindProperty<TValue>)GetAdapterInstance(source, targetType, converter);
        }

        private static object GetAdapterInstance(IBindProperty source, Type targetType, IPropertyConverter converter) =>
            Activator.CreateInstance(GetConstructedType(source.ValueType, targetType), source, converter);
        private static Type GetConstructedType(Type sourceType, Type targetType) =>
            BindPropertyAdapterType.MakeGenericType(sourceType, targetType);




    }
}