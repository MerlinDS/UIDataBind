using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Components
{
    [HideBindingValue]
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UIDataBind/Sprite", 1)]
    public sealed class SpriteBinding: BasePropertyBindingWithComponent<Image, Sprite>
    {
#pragma warning disable 0649
        [SerializeField, Tooltip("The path to sprite asset, will be used when a binding type is None")]
        [BindingValue]
        private string _sprite;
#pragma warning restore 0649

        protected override Sprite DefaultValue =>
            Path.Type == BindingType.None ? Resources.Load<Sprite>(_sprite) : null;

        protected override void UpdateValueHandler(Sprite value) => Component.sprite = value;
    }
}