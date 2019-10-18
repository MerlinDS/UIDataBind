using UnityEngine;
using UnityEngine.UI;

namespace UIDataBind.Binders.ValueBinders
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UIDataBind/Sprite", 1)]
    public class SpriteBinder : ValueToComponentBinder<Image, Sprite>
    {
#pragma warning disable 0649
        [SerializeField, Tooltip("The path to sprite asset, will be used when a binding type is None")]
        private string _sprite;
#pragma warning restore 0649
        protected override void UpdateValueHandler(Sprite value)
        {
            Component.sprite = value;
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