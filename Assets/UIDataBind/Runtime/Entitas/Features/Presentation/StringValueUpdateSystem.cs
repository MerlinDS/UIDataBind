using System;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class StringValueUpdateSystem : ValueUpdateSystem<string>
    {
        public StringValueUpdateSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(string sourceValue, Type targetType, out object result)
        {
            result = default;
            if (targetType == typeof(bool))
            {
                result = Convert.ToBoolean(sourceValue);
                return true;
            }

            if (targetType == typeof(int))
            {
                result = Convert.ToInt32(sourceValue);
                return true;
            }

            if (targetType == typeof(float))
            {
                result = Convert.ToSingle(sourceValue);
                return true;
            }

            /*if (targetType == typeof(string))
            {
                result = Convert.ToString(sourceValue);
                return true;
            }*/

            return false;
        }

        protected override bool TryConvertTargetToSource(object targetValue, out string result)
        {
            try
            {
                result = Convert.ToString(targetValue);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                result = string.Empty;
                return false;
            }
        }
    }
}