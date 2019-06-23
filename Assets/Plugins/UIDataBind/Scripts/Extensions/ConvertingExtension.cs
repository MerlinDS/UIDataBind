using System;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Extensions
{
    public static class ConvertingExtension
    {
        // ReSharper disable once UnusedTypeParameter
        public static bool IsConvertible<TValue>(this IBindingProperty property)
        {
            var valueType = property.Value.GetType();
            if (typeof(IConvertible).IsAssignableFrom(valueType))
                return true;

            //TODO: Implement other conversion checks
            return false;
        }

        public static object ConvertTo<TType>(this IBindingProperty property)
        {
            var expectedType = typeof(TType);
            if (property.Value is IConvertible value)
                return ConvertValue(expectedType, value);

            //TODO: Implement other conversions
            return default;
        }

        private static object ConvertValue(Type expectedType,  IConvertible value)
        {
            if (expectedType == typeof(bool))
                return value.ToBoolean(null);
            if (expectedType == typeof(int))
                return value.ToInt32(null);
            if (expectedType == typeof(float))
                return value.ToSingle(null);
            if (expectedType == typeof(double))
                return value.ToDouble(null);
            if (expectedType == typeof(string))
                return value.ToString(null);
            return default;
        }
    }
}