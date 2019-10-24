using UIDataBind.Base;
using UIDataBind.Binders.ValueBinders;
using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.Buttons
{
    [AddComponentMenu("UIDataBind/Button - Click", 1)]
    [RequireComponent(typeof(Button))]
    public class ButtonClickBinder : ValueToComponentBinder<Button, ControlEvent>
    {
        protected override void Bind()
        {
            Component?.onClick.AddListener(InvokeAction);
        }


        protected override void Unbind()
        {
            Component?.onClick.RemoveListener(InvokeAction);
        }

        protected override void UpdateValueHandler(ControlEvent value)
        {
        }

        private void InvokeAction()
        {
            Value = ControlEvent.Click;
            BroadcastEvent(ControlEvent.Click);
        }
    }
}