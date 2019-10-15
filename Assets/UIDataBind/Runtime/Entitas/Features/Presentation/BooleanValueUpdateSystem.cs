using System;

namespace UIDataBind.Entitas.Features.Presentation
{
    public class BooleanValueUpdateSystem : ValueUpdateSystem<bool>
    {
        public BooleanValueUpdateSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(bool sourceValue, Type targetType, out object result) =>
            InternalConverter.TryConvertSourceToTarget(sourceValue, targetType, out result);

        protected override bool TryConvertTargetToSource(object targetValue, out bool result) =>
            InternalConverter.TryConvertTargetToSource(targetValue, Convert.ToBoolean, out result);
    }
}