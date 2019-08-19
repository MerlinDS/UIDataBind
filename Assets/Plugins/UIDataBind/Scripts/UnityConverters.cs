using UIDataBindCore;
using UnityEngine;

namespace Plugins.UIDataBind
{
    public static class UnityConverters
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Register()
        {
            var conversionMethods = BindingKernel.Instance.ConversionMethods;
            conversionMethods.Register<Sprite, string>(Resources.Load<Sprite>,
                                                       sprite => sprite != null ? sprite.name : string.Empty);
        }
    }
}