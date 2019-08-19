using UnityEngine;
using UnityEngine.UI;

namespace Plugins.UIDataBind.Binders
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UIDataBind/Sprite", 1)]
    public sealed class SpriteBinder : PropertyBinderWithComponent<Image, Sprite>
    {
        protected override void UpdateValueHandler(Sprite value) => Component.sprite = value;
    }
}