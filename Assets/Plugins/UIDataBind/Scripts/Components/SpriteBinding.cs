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

        protected override Sprite DefaultValue =>
            Path.Type == BindingType.None ? Resources.Load<Sprite>(_sprite) : null;

        protected override void UpdateValueHandler(Sprite value) => Component.sprite = value;
    }
}