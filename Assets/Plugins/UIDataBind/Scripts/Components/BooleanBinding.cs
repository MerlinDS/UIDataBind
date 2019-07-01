using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Boolean", 1)]
    public class BooleanBinding :  BasePropertyBindingBehaviour<bool>
    {
        protected override void UpdateValueHandler(bool value)
        {

        }
    }
}