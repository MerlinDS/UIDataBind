using TMPro;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    [AddComponentMenu("UIDataBind/TMP - String", 1)]
    [RequireComponent(typeof(TextMeshProUGUI))]
    // ReSharper disable once InconsistentNaming
    public class StringBinder : PropertyBinderWithComponent<TextMeshProUGUI, string>
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