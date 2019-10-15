using System;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class IntValueUpdateSystem : ValueUpdateSystem<int>
    {
        public IntValueUpdateSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(int sourceValue, Type targetType, out object result) =>
            InternalConverter.TryConvertSourceToTarget(sourceValue, targetType, out result);

        protected override bool TryConvertTargetToSource(object targetValue, out int result) =>
            InternalConverter.TryConvertTargetToSource(targetValue, Convert.ToInt32, out result);
    }
}