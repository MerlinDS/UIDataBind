using UIDataBind.Entitas;

namespace UIDataBind.Base.Extensions
{
    public static class ECSEngineExtension
    {
        private static readonly IECSEngine Engine
#if ECS_ENTITAS
            = new EntitasEngine();
#endif
        //TODO: Add DOTS as Engine

        public static IECSEngine GetEngine(this IEngineProvider binder) =>
            Engine;

    }
}