namespace UIDataBind.Base
{
    public interface IBinder
    {
        void Bind();
        void Unbind();
        IEntityProvider Engine { get; set; }
        string Path { get; }
    }
}