using System;
using JetBrains.Annotations;
using Plugins.UIDataBind.Base;
using UnityEngine;

namespace Plugins.UIDataBind.Utils
{
    public static class ConvertUtils
    {

        public static bool IsConvertible<TValue>([NotNull] this IBindingProperty property) =>
            property.IsConvertible(typeof(TValue));

        public static bool IsConvertible([NotNull] this IBindingProperty property, Type expectedType)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            return IsConvertible(property.GetValueType, expectedType);
        }

        public static bool IsConvertible([NotNull] Type convertible, [NotNull] Type expectedType)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (typeof(IConvertible).IsAssignableFrom(convertible))
            {
                if(typeof(IConvertible).IsAssignableFrom(expectedType))
                    return true;

                if (typeof(Sprite).IsAssignableFrom(expectedType))
                    return convertible == typeof(string);

                /*if (typeof(UnityEngine.Color).IsAssignableFrom(expectedType))
                    return convertible == typeof(int) || convertible == typeof(bool);*/

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

                if (typeof(Sprite).IsAssignableFrom(expectedType))
                    return ConvertValue(convertible);
            }
            //TODO: Implement other conversions
            return default;
        }

        private static Sprite ConvertValue(IConvertible value)
        {
            var path = (string)ConvertValue(typeof(string), value);
            return Resources.Load<Sprite>(path);
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


        public static TType SafeCast<TType>(object value)
        {
            switch (value)
            {
                case null:
                    return default;
                case TType concreteData:
                    return concreteData;
                default:
                    Debug.LogError($"Specified cast is not valid: \"{value}\" to {typeof(TType)}");
                    return default;
            }
        }
    }
}