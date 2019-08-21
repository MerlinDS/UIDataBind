using Plugins.UIDataBind.Attributes;
using TMPro;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    [HideBinderValue(BindingType.Context)]
    [AddComponentMenu("UIDataBind/TMP - Input String", 1)]
    [RequireComponent(typeof(TMP_InputField))]
    public class InputStringBinder: PropertyBinderWithComponent<TMP_InputField, string>
    {
        [SerializeField]
        private string _format = "{0}";

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

        private void ComponentHandler(string value) =>
            Value = value;
        protected override void UpdateValueHandler(string value)
        {
            if (!string.IsNullOrEmpty(_format))
                value = string.Format(_format, value);
            Component.text = value;
        }
    }
}