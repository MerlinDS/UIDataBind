using Plugins.UIDataBind.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Components
{
    [AddComponentMenu("UIDataBind/Sprite", 1)]
    [RequireComponent(typeof(Image))]
    public sealed class SpriteBinding: BasePropertyBindingWithComponentBehaviour<Image, Sprite>
    {
#pragma warning disable 0649
        [SerializeField]
        private string _sprite;
#pragma warning restore 0649

        protected override void UpdateValueHandler(Sprite value)
        {
            if (Path.Type == BindingType.None)
                value = Resources.Load<Sprite>(_sprite);
            Component.sprite = value;
        }
    }
}