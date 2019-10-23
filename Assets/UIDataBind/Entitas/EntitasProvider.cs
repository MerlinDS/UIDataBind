using UIDataBind.Base;

namespace UIDataBind.Entitas
{
    internal class EntitasProvider : IEntityProvider
    {
        private readonly UiBindEntity _entity;

        public EntitasProvider(UiBindEntity entity) =>
            _entity = entity;

        public void SetDirty() =>
            _entity.isDirty = true;

        public void Destroy() =>
            _entity.Destroy();

        public void BroadcastEvent(ControlEvent type)
        {
            _entity.ReplaceEvent(type);
            SetDirty();
        }
    }
}