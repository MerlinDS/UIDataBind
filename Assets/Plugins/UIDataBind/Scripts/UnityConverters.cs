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
            conversionMethods.Register<Sprite, string>(Resources.Load<Sprite>,a => a.GetName());
            conversionMethods.Register<Texture, string>(Resources.Load<Texture>,a => a.GetName());
            conversionMethods.Register<Texture2D, string>(Resources.Load<Texture2D>,a => a.GetName());
            conversionMethods.Register<Texture, Texture2D>(a => (Texture) a, a => (Texture2D) a);
        }

        private static string GetName(this Object obj) =>
            obj != null ? obj.name : string.Empty;
    }
}