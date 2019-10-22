using UIDataBind.Runtime.Base.Extensions;
using UnityEngine;

namespace UIDataBind.Entitas
{
    public static class UIDataBindBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Starter() => ECSEngineExtension.Register<EntitasEngine>();
    }
}