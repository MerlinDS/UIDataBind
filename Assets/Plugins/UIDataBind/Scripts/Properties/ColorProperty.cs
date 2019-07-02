using UnityEngine;

namespace Plugins.UIDataBind.Properties
{
    public class ColorProperty : BindingProperty<Color>
    {
        public ColorProperty(Color value = default) : base(value)
        {
        }
    }
}