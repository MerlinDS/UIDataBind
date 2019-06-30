using UnityEngine;

namespace Plugins.UIDataBind.Properties
{
    public class SpriteProperty : BaseBindingProperty<Sprite>
    {
        public SpriteProperty(Sprite value = default) : base(value)
        {
        }
    }
}