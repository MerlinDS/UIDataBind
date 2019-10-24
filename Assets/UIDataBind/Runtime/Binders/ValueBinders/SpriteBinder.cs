using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UIDataBind/Sprite", 1)]
    public sealed class SpriteBinder : ValueToComponentBinder<Image, Sprite>
    {
#pragma warning disable 0649
        [SerializeField]
        [Tooltip("The path to sprite asset, will be used when a binding type is None")]
        private string _sprite;
#pragma warning restore 0649

        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Resources.Load<Sprite>(_sprite));
        }

        protected override void UpdateValueHandler(Sprite value)
        {
            Component.sprite = value;
            if (value != null)
                _sprite = value.name;
        }
    }
}