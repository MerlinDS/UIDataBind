using UnityEngine;

namespace Plugins.UIDataBind.Properties
{
    public class ColorProperty : BaseBindingProperty<Color>
    {
        public ColorProperty(Color value = default) : base(value)
        {
        }
    }
}