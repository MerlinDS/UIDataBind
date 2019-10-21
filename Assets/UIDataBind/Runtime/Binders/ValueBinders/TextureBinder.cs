using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(RawImage))]
    [AddComponentMenu("UIDataBind/Texture", 1)]
    public class TextureBinder : ValueToComponentBinder<RawImage, Texture>
    {
#pragma warning disable 0649
        [SerializeField, Tooltip("The path to sprite asset, will be used when a binding type is None")]
        private string _sprite;
#pragma warning restore 0649
        protected override void UpdateValueHandler(Texture value)
        {
            Component.texture = value;
            if(value != null)
                _sprite = value.name;
        }

        /*protected override void Bind()
        {
            base.Bind();
            if (BindingType == BindingType.None)
                Value = Resources.Load<Sprite>(_sprite);
        }*/
    }
}