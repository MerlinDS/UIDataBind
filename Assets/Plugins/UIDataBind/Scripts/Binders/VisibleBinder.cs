using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    [AddComponentMenu("UIDataBind/Visible", 0)]
    public class VisibleBinder : PropertyBinder<bool>
    {
        protected override void UpdateValueHandler(bool value)
        {
            Debug.Log(value);
        }
    }
}