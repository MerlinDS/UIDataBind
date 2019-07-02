using UnityEngine;

namespace Plugins.UIDataBind.Properties
{
    public class SpriteProperty : BindingProperty<Sprite>
    {
        public SpriteProperty(Sprite value = default) : base(value)
        {
        }
    }
}