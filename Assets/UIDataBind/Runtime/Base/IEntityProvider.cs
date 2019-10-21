namespace UIDataBind.Base
{
    public interface IEntityProvider
    {
        void SetDirty();
        void Destroy();
        void BroadcastEvent(ControlEvent type, BindingPath path);
    }
}