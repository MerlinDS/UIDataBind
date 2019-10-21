using UIDataBind.Base;

namespace UIDataBind.Entitas.Wrappers
{
    public class EntitasProvider : IEntityProvider
    {
        private readonly UiBindEntity _entity;

        public EntitasProvider(UiBindEntity entity) =>
            _entity = entity;

        public void SetDirty() =>
            _entity.isDirty = true;

        public void Destroy() =>
            _entity.Destroy();

        public void BroadcastEvent(ControlEvent type, BindingPath path)
        {
            _entity.ReplaceEvent(type, path);
            SetDirty();
        }
    }
}