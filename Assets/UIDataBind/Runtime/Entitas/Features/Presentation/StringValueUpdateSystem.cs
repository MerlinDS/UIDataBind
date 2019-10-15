using System;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class StringValueUpdateSystem : ValueUpdateSystem<string>
    {
        public StringValueUpdateSystem(UiBindContext context) : base(context)
        {
        }

        protected override bool TryConvertSourceToTarget(string sourceValue, Type targetType, out object result) =>
            InternalConverter.TryConvertSourceToTarget(sourceValue, targetType, out result);

        protected override bool TryConvertTargetToSource(object targetValue, out string result)=>
            InternalConverter.TryConvertTargetToSource(targetValue, Convert.ToString, out result);
    }
}