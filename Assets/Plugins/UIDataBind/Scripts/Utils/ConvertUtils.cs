using System;
using JetBrains.Annotations;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Utils
{
    public static class ConvertUtils
    {

        // ReSharper disable once UnusedTypeParameter
        public static bool IsConvertible<TValue>([NotNull] this IBindingProperty property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (typeof(IConvertible).IsAssignableFrom(property.GetValueType))
            {
                var expectedType = typeof(TValue);
                if(typeof(IConvertible).IsAssignableFrom(expectedType))
                    return true;

                if (typeof(UnityEngine.Sprite).IsAssignableFrom(expectedType))
                    return true;

            }

            //TODO: Implement other conversion checks
            return false;
        }

        public static TType ConvertTo<TType>(object value) =>
            (TType) ConvertTo(typeof(TType), value);

        public static object ConvertTo(Type expectedType, object value)
        {
            if (value is IConvertible convertible)
            {
                if(typeof(IConvertible).IsAssignableFrom(expectedType))
                    return ConvertValue(expectedType, convertible);

                if (typeof(UnityEngine.Sprite).IsAssignableFrom(expectedType))
                    return ConvertValue(convertible);
            }
            //TODO: Implement other conversions
            return default;
        }

        private static UnityEngine.Sprite ConvertValue(IConvertible value)
        {
            var path = (string)ConvertValue(typeof(string), value);
            return UnityEngine.Resources.Load<UnityEngine.Sprite>(path);
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