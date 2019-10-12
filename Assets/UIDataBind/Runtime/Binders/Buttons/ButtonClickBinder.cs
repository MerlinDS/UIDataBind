using UIDataBind.Entitas.Components;
using UIDataBind.Entitas.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.Buttons
{
    [AddComponentMenu("UIDataBind/Button - Click", 1)]
    [RequireComponent(typeof(Button))]
    public class ButtonClickBinder : BaseBinder
    {
        public override void Bind() =>
            GetComponent<Button>()?.onClick.AddListener(InvokeAction);


        public override void Unbind() =>
            // ReSharper disable once Unity.NoNullPropagation
            GetComponent<Button>()?.onClick.RemoveListener(InvokeAction);

        private void InvokeAction() => this.CreateAction(ActionType.Click);
    }


}