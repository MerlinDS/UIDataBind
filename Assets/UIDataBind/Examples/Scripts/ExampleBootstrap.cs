using UIDataBind.Entitas.Features;
using UIDataBind.Examples.Game;
using UIDataBind.Examples.Scripts;
using UIDataBind.Examples.Utils.Entitas;
using UnityEngine;

namespace UIDataBind.Examples
{
    public static class ExampleBootstrap
    {
        private static GameManager _manager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Start()
        {
            _manager = Contexts.sharedInstance.GetGameManager(SystemsModule);
            _manager.Start();
        }

        private static SystemsModule SystemsModule => new SystemsModule()
            .Bind<UIDataBindingSystems>()
            .Bind<GameSystems>();
    }
}