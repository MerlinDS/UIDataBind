using Plugins.UIDataBind.Attributes;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    [ShowBindingValue]
    [AddComponentMenu("UIDataBind/Boolean", 1)]
    public class BooleanBinding :  BasePropertyBindingBehaviour<bool>
    {
        protected override void UpdateValueHandler(bool value)
        {

        }
    }
}