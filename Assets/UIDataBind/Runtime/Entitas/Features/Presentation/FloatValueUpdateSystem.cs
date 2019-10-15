using System;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class FloatValueUpdateSystem : ValueUpdateSystem<float>
    {
        public FloatValueUpdateSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(float sourceValue, Type targetType, out object result) =>
            InternalConverter.TryConvertSourceToTarget(sourceValue, targetType, out result);

        protected override bool TryConvertTargetToSource(object targetValue, out float result) =>
            InternalConverter.TryConvertTargetToSource(targetValue, Convert.ToSingle, out result);
    }
}