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
            if (InternalConverter.TryConvertSourceToTarget(sourceValue, targetType, out result))
                return true;

            if (targetType == typeof(Sprite))
            {
                result = Resources.Load<Sprite>(sourceValue);
                return true;
            }
            if (targetType == typeof(Texture))
            {
                result = Resources.Load<Texture>(sourceValue);
                return true;
            }
            return false;
        }

        protected override bool TryConvertTargetToSource(object targetValue, out string result)=>
            InternalConverter.TryConvertTargetToSource(targetValue, Convert.ToString, out result);
    }
}