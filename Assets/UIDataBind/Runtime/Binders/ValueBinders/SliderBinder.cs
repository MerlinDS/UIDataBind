using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("UIDataBind/Slider", 1)]
    public class SliderBinder : ValueToComponentBroadcastBinder<Slider, float>
    {
        protected override void Bind()
        {
            base.Bind();
            Component.onValueChanged.AddListener(ComponentHandler);
        }

        protected override void Unbind()
        {
            Component.onValueChanged.RemoveAllListeners();
            base.Unbind();
        }
        protected override void UpdateValueHandler(float value) =>
            Component.value = value;
    }
}