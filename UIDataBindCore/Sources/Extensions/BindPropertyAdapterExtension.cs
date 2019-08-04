using System;
using System.Runtime.CompilerServices;
using UIDataBindCore.Converters;
using UIDataBindCore.Properties;

namespace UIDataBindCore.Extensions
{
    public static class BindPropertyAdapterExtension
    {
        private static readonly Type BindPropertyAdapterType = typeof(BindPropertyAdapter<,>);

        public static IBindProperty<TValue> AsPropertyOf<TValue>(this IConversionMethods converters,
            IBindProperty source)
        {
            if (converters == null)
                throw new ArgumentNullException(nameof(converters));

            if (source == null)
                return default;

            var targetType = typeof(TValue);
            if (targetType == source.ValueType)
                return (IBindProperty<TValue>) source;

            if (!converters.Has(targetType, source.ValueType))
                return default;

            return (IBindProperty<TValue>) GetAdapterInstance(converters, source, targetType);
        }

        private static object GetAdapterInstance(this IConversionMethods converters, IBindProperty source,
            Type targetType) =>
            Activator.CreateInstance(GetConstructedType(source.ValueType, targetType),
                                     converters.GetArguments(source, targetType));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type GetConstructedType(Type sourceType, Type targetType) =>
            BindPropertyAdapterType.MakeGenericType(sourceType, targetType);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object[] GetArguments(this IConversionMethods converters, IBindProperty source,
            Type targetType) => new object[]
        {
            source,
            converters.Retrieve(source.ValueType, targetType),
            converters.Retrieve(targetType, source.ValueType)
        };
    }
}