using Plugins.UIDataBind.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{
    [HideBinderValue]
    [RequireComponent(typeof(RawImage))]
    [AddComponentMenu("UIDataBind/Texture", 1)]
    public class TextureBinder: PropertyBinderWithComponent<RawImage, Texture>
    {
#pragma warning disable 0649
        [SerializeField, Tooltip("The path to texture asset, will be used when a binding type is None")]
        private string _texture;
#pragma warning restore 0649
        protected override void UpdateValueHandler(Texture value)
        {
            Component.texture = value;
            if(value != null && BindingType != BindingType.None)
                _texture = value.name;
        }

        public override void Bind()
        {
            base.Bind();
            if (BindingType == BindingType.None)
                Value = Resources.Load<Texture>(_texture);
        }
    }
}