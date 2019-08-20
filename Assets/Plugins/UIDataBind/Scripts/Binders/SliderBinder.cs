using Plugins.UIDataBind.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{
    [HideBinderValue(BindingType.Context)]
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("UIDataBind/Slider", 1)]
    public class SliderBinder : PropertyBinderWithComponent<Slider, float>
    {
        public override void Bind()
        {
            base.Bind();
            Component.onValueChanged.AddListener(SliderHandler);
        }

        public override void Unbind()
        {
            Component.onValueChanged.RemoveAllListeners();
            base.Unbind();
        }

        private void SliderHandler(float value)
        {
            Value = value;
        }

        protected override void UpdateValueHandler(float value) =>
            Component.value = value;
    }
}