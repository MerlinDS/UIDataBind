using Entitas;

namespace UIDataBind.Entitas.Features.Presentation
{
    public class InitValueBindersSystem : IInitializeSystem, ITearDownSystem
    {
        private readonly UiBindContext _context;
        private IGroup<UiBindEntity> _bindersGroup;

        public InitValueBindersSystem(UiBindContext context) =>
            _context = context;

        public void Initialize()
        {
            _bindersGroup = _context.GetGroup(UiBindMatcher.AllOf(UiBindMatcher.Binder, UiBindMatcher.BindingPath));
            _bindersGroup.OnEntityAdded += OnEntityAddedHandler;
            foreach (var entity in _bindersGroup.GetEntities())
                SetDirtyModel(entity);
        }

        public void TearDown() =>
            _bindersGroup.OnEntityAdded -= OnEntityAddedHandler;

        private void OnEntityAddedHandler(IGroup<UiBindEntity> g, UiBindEntity e, int i, IComponent c) =>
            SetDirtyModel(e);

        private void SetDirtyModel(UiBindEntity entity)
        {
            if(!entity.hasBindingPath)
                return;

            var modelEntity = _context.GetEntityWithModelPath(entity.bindingPath.Value);
            if(modelEntity != null)
                modelEntity.isDirty = true;
        }
    }
}