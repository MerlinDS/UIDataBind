using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("UIDataBind/Toggle", 1)]
    public sealed class ToggleBinder : ValueToComponentBinder<Toggle, bool>
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

        private void ComponentHandler(bool value) =>
            Value = value;

        protected override void UpdateValueHandler(bool value) =>
            Component.isOn = value;


    }
}