namespace UIDataBind.Base
{
    public interface IEntityProvider
    {
        void SetDirty();
        void Destroy();
        void BroadcastEvent(UIEventType type);
    }
}