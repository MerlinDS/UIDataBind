using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    [AddComponentMenu("UIDataBind/Alpha", 1)]
    [RequireComponent(typeof(CanvasGroup))]
    public class AlphaBinder : ValueToComponentBinder<CanvasGroup, float>
    {
        protected override void UpdateValueHandler(float value) =>
            Component.alpha = value;
    }
}