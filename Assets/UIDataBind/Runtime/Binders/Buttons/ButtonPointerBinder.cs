using UIDataBind.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIDataBind.Binders.Buttons
{
    [AddComponentMenu("UIDataBind/Button - Click & Hover", 1)]
    [RequireComponent(typeof(Button))]
    public sealed class ButtonPointerBinder : ButtonClickBinder, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Value = true;
            BroadcastEvent(UIEventType.PointerEnter);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Value = true;
            BroadcastEvent(UIEventType.PointerExit);
        }
    }
}