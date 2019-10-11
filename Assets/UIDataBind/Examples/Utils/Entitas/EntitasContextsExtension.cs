using Entitas;
using UIDataBind.Examples.Scripts;

namespace UIDataBind.Examples.Utils.Entitas
{
    public static class EntitasContextsExtension
    {
        public static GameManager GetGameManager(this IContexts contexts, SystemsModule module) =>
            new GameManager(contexts, module);
    }
}