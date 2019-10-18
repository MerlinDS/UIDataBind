using System;
using UnityEngine;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class SpriteValueBinderSystem : ValueUpdateSystem<Sprite>
    {
        public SpriteValueBinderSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(Sprite sourceValue, Type targetType, out object result)
        {
            if (targetType == typeof(string))
            {
                result = sourceValue.name;
                return true;
            }

            result = default;
            return false;
        }

        protected override bool TryConvertTargetToSource(object targetValue, out Sprite result)
        {
            if (targetValue is string path)
            {
                result = Resources.Load<Sprite>(path);
                return true;
            }

            result = default;
            return false;
        }
    }
}