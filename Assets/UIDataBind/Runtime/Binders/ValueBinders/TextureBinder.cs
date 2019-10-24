using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(RawImage))]
    [AddComponentMenu("UIDataBind/Texture", 1)]
    public sealed class TextureBinder : ValueToComponentBinder<RawImage, Texture>
    {
#pragma warning disable 0649
        [SerializeField]
        [Tooltip("The path to sprite asset, will be used when a binding type is None")]
        private string _texture;
#pragma warning restore 0649

        protected override void Bind()
        {
            if (BindingType == BindingType.Self)
                UpdateValueHandler(Resources.Load<Texture>(_texture));
        }

        protected override void UpdateValueHandler(Texture value)
        {
            Component.texture = value;
            if (value != null)
                _texture = value.name;
        }
    }
}