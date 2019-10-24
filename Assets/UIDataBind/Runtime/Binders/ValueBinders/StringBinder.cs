using TMPro;
using UIDataBind.Binders.Attributes;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    [ShowBinderValue(BindingType.Self)]
    [AddComponentMenu("UIDataBind/TMP - String", 1)]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class StringBinder : ValueToComponentBinder<TextMeshProUGUI, string>
    {
        [SerializeField]
        private string _format = "{0}";

        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Value);
        }

        protected override void UpdateValueHandler(string value)
        {
            if (!string.IsNullOrEmpty(_format))
                value = string.Format(_format, value);
            Component.text = value;
        }
    }
}