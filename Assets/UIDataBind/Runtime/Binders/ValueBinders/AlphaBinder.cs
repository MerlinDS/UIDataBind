using UIDataBind.Binders.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [ShowBinderValue(BindingType.Self)]
    [AddComponentMenu("UIDataBind/Alpha - Graphic", 1)]
    [RequireComponent(typeof(Graphic))]
    public sealed class AlphaBinder : ValueToComponentBinder<Graphic, float>
    {
        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Value);
        }

        protected override void UpdateValueHandler(float value)
        {
            var color = Component.color;
            color.a = value;
            Component.color = color;
        }
    }
}