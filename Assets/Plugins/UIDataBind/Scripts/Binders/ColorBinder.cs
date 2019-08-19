using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{
    [AddComponentMenu("UIDataBind/Color", 1)]
    public class ColorBinder : PropertyBinderWithComponent<Graphic, Color>
    {
        protected override void UpdateValueHandler(Color value) => Component.color = value;

        private void Reset() => Value = Color.white;
    }
}