using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [AddComponentMenu("UIDataBind/Color", 1)]
    public class ColorBinder : ValueToComponentBinder<Graphic, Color>
    {
        protected override void UpdateValueHandler(Color value) =>
            Component.color = value;

        private void Reset() => Value = Color.white;
    }
}