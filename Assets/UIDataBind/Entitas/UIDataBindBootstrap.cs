using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UnityEngine;

namespace UIDataBind.Entitas
{
    public static class UIDataBindBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Starter() => ECSEngineExtension.Register<EntitasEngine>();
    }
}
public sealed partial class UiBindContext : IEngineProvider
{

}