using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{

    [AddComponentMenu("UIDataBind/Action - Button", 1)]
    [RequireComponent(typeof(Button))]
    public class ButtonToActionBinder : ActionBinder
    {
        private Button _button;

        protected override void Subscribe()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            _button.onClick.AddListener(InvokeAction);
        }

        protected override void Unsubscribe()
        {
            if (_button == null)
                return;

            _button.onClick.RemoveListener(InvokeAction);
        }
    }
}