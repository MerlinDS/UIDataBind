using Plugins.UIDataBind.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{
    [HideBinderValue(BindingType.Context)]
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("UIDataBind/Toggle", 1)]
    public sealed class ToggleBinder : PropertyBinderWithComponent<Toggle, bool>
    {

        public override void Bind()
        {
            base.Bind();
            Component.onValueChanged.AddListener(ComponentHandler);
        }

        public override void Unbind()
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