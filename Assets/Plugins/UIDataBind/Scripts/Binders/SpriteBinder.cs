using Plugins.UIDataBind.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{
    [HideBinderValue]
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UIDataBind/Sprite", 1)]
    public sealed class SpriteBinder : PropertyBinderWithComponent<Image, Sprite>
    {
#pragma warning disable 0649
        [SerializeField, Tooltip("The path to sprite asset, will be used when a binding type is None")]
        private string _sprite;
#pragma warning restore 0649
        protected override void UpdateValueHandler(Sprite value)
        {
            Component.sprite = value;
            if(value != null && BindingType != BindingType.None)
                _sprite = value.name;
        }

        public override void Bind()
        {
            base.Bind();
            if (BindingType == BindingType.None)
                Value = Resources.Load<Sprite>(_sprite);
        }
    }
}