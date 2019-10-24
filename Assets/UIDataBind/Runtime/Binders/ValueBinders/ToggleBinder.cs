using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("UIDataBind/Toggle", 1)]
    public sealed class ToggleBinder : ValueToComponentBroadcastBinder<Toggle, bool>
    {
        protected override void Bind()
        {
            Component.onValueChanged.AddListener(ComponentHandler);
        }

        protected override void Unbind()
        {
            Component.onValueChanged.RemoveAllListeners();
        }

        protected override void UpdateValueHandler(bool value)
        {
            Component.SetIsOnWithoutNotify(value);
        }
    }
}