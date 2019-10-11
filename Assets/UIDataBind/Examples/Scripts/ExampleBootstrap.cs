using UIDataBind.Examples.Scripts.Game;
using UIDataBind.Examples.Utils.Entitas;
using UnityEngine;

namespace UIDataBind.Examples.Scripts
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
            .Bind<GameSystems>();
    }
}