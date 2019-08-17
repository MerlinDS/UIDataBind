namespace UIDataBindCore
{
    public interface IBinder
    {
        IDataContext Context { get; }
        void Bind();
        void Unbind();
    }
}