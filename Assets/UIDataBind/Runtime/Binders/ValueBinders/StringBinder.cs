using TMPro;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    [AddComponentMenu("UIDataBind/TMP - String", 1)]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class StringBinder : ValueToComponentBinder<TextMeshProUGUI, string>
    {
        [SerializeField]
        private string _format = "{0}";
        protected override void UpdateValueHandler(string value)
        {
            if (!string.IsNullOrEmpty(_format))
                value = string.Format(_format, value);
            Component.text = value;
        }
    }
}