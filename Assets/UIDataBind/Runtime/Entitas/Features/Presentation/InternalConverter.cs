using System;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    public static class InternalConverter
    {
        public static bool TryConvertSourceToTarget<T>(T sourceValue, Type targetType, out object result)
        {
            var sourceType = typeof(T);
            if (sourceType != targetType && targetType == typeof(bool))
            {
                result = Convert.ToBoolean(sourceValue);
                return true;
            }

            if (sourceType != targetType && targetType == typeof(int))
            {
                result = Convert.ToInt32(sourceValue);
                return true;
            }

            if (sourceType != targetType && targetType == typeof(float))
            {
                result = Convert.ToSingle(sourceValue);
                return true;
            }

            if (sourceType != targetType && targetType == typeof(string))
            {
                result = Convert.ToString(sourceValue);
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryConvertTargetToSource<T>(object targetValue, Func<object, T> action, out T result)
        {
            try
            {
                result = action.Invoke(targetValue);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                result = default;
                return false;
            }
        }
    }
}