using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.Buttons
{
    [AddComponentMenu("UIDataBind/Button - Click", 1)]
    [RequireComponent(typeof(Button))]
    public class ButtonClickBinder : BaseBinder
    {
        protected override void Bind() =>
            GetComponent<Button>()?.onClick.AddListener(InvokeAction);


        protected override void Unbind() =>
            // ReSharper disable once Unity.NoNullPropagation
            GetComponent<Button>()?.onClick.RemoveListener(InvokeAction);

        protected override void Dispose()
        {
            
        }

        private void InvokeAction() => Debug.Log("Click");
    }


}