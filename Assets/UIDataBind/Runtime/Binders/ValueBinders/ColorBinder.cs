using UIDataBind.Binders.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [ShowBinderValue(BindingType.Self)]
    [AddComponentMenu("UIDataBind/Color", 1)]
    public sealed class ColorBinder : ValueToComponentBinder<Graphic, Color>
    {
        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Value);
        }

        protected override void UpdateValueHandler(Color value)
        {
            Component.color = value;
        }

        private void Reset()
        {
            Value = Color.white;
        }
    }
}