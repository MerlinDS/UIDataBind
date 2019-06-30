using System;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Utils
{
    public static class ConvertUtils
    {

        // ReSharper disable once UnusedTypeParameter
        public static bool IsConvertible<TValue>(this IBindingProperty property)
        {
            var valueType = property.Value.GetType();
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (typeof(IConvertible).IsAssignableFrom(valueType))
                return true;

            //TODO: Implement other conversion checks
            return false;
        }

        public static TType ConvertTo<TType>(object value) =>
            (TType) ConvertTo(typeof(TType), value);

        public static object ConvertTo(Type expectedType, object value)
        {
            if (value is IConvertible convertible)
                return ConvertValue(expectedType, convertible);
            //TODO: Implement other conversions
            return default;
        }

        private static object ConvertValue(Type expectedType, IConvertible value)
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