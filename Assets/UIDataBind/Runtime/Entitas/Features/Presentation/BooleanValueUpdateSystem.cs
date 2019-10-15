using System;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    public class BooleanValueUpdateSystem : ValueUpdateSystem<bool>
    {
        public BooleanValueUpdateSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(bool sourceValue, Type targetType, out object result)
        {
            result = null;
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

            if (targetType == typeof(string))
            {
                result = Convert.ToString(sourceValue);
                return true;
            }

            return false;
        }

        protected override bool TryConvertTargetToSource(object targetValue, out bool result)
        {
            try
            {
                result = Convert.ToBoolean(targetValue);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                result = false;
                return false;
            }
        }
    }
}