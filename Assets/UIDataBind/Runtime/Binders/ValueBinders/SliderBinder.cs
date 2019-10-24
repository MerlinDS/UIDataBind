using UIDataBind.Binders.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [ShowBinderValue(BindingType.Self)]
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("UIDataBind/Slider", 1)]
    public sealed class SliderBinder : ValueToComponentBroadcastBinder<Slider, float>
    {
        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Value);
            Component.onValueChanged.AddListener(ComponentHandler);
        }

        protected override void Unbind()
        {
            Component.onValueChanged.RemoveAllListeners();
        }

        protected override void UpdateValueHandler(float value)
        {
            Component.value = value;
        }
    }
}