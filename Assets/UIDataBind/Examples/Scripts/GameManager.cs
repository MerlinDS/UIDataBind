using Entitas;
using UIDataBind.Examples.Utils.Entitas;
using UniRx;
using UnityEngine;

namespace UIDataBind.Examples.Scripts
{
    public class GameManager
    {
        private readonly IContexts _contexts;
        private readonly SystemsModule _systems;
        private readonly CompositeDisposable _disposable;

        public GameManager(IContexts contexts, SystemsModule systems)
        {
            _contexts = contexts;
            _systems = systems;
            _disposable = new CompositeDisposable();
        }

        public void Start()
        {
            Debug.Log("Start new game session");
            Initialize();
            Observable.EveryUpdate().Subscribe(_ => Execute()).AddTo(_disposable);
            Observable.EveryLateUpdate().Subscribe(_ =>Cleanup()).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            foreach (var system in _systems)
                system.TearDown();

            foreach (var context in _contexts.allContexts)
            {
                context.RemoveAllEventHandlers();
                context.DestroyAllEntities();
                context.Reset();
            }
        }

        private void Initialize()
        {
            _systems.InstantiateAll(_contexts);
            foreach (var system in _systems)
                system.Initialize();
        }

        private void Execute()
        {
            foreach (var system in _systems)
                system.Execute();
        }

        private void Cleanup()
        {
            foreach (var system in _systems)
                system.Cleanup();
        }


    }
}