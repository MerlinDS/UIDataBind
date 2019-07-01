using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Action - Button", 1)]
    [RequireComponent(typeof(Button))]
    public class ButtonActionBinding : BaseActionBinding
    {
        private Button _button;

        protected override void Subscribe()
        {
            if (_button == null)
                _button = GetComponent<Button>();

            _button.onClick.AddListener(OnClick);
        }

        protected override void Unsubscribe()
        {
            if (_button == null)
                return;

            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick() => ExternalAction?.Invoke();
    }
}