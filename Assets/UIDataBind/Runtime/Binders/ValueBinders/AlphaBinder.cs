using UIDataBind.Binders.Attributes;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    [ShowBinderValue(BindingType.Self)]
    [AddComponentMenu("UIDataBind/Alpha", 1)]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class AlphaBinder : ValueToComponentBinder<CanvasGroup, float>
    {
        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Value);
        }

        protected override void UpdateValueHandler(float value)
        {
            Component.alpha = value;
        }
    }
}