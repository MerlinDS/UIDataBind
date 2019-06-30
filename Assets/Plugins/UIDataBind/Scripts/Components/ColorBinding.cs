using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Color", 1)]
    public sealed class ColorBinding: BasePropertyBindingWithComponentBehaviour<Graphic, Color>
    {
        protected override void UpdateValueHandler(Color value) => Component.color = value;

        private void Reset() => Value = Color.white;
    }
}