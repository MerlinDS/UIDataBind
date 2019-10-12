using UIDataBind.Entitas.Components;
using UIDataBind.Entitas.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIDataBind.Binders.Buttons
{
    [AddComponentMenu("UIDataBind/Button - Click & Hover", 1)]
    [RequireComponent(typeof(Button))]
    public sealed class ButtonPointerBinder : ButtonClickBinder, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData) =>
            this.CreateAction(ActionType.PointerEnter);

        public void OnPointerExit(PointerEventData eventData) =>
            this.CreateAction(ActionType.PointerExit);
    }
}