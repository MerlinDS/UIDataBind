using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Visible", 0)]
    public sealed class VisibleBinding : BasePropertyBindingBehaviour<bool>
    {
        [SerializeField]
        private bool _isVisibleOnStart;

        private void Start() => Value = _isVisibleOnStart;

        protected override void UpdateValueHandler(bool value)
        {
        }
    }
}