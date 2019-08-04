namespace UIDataBindCore
{
    public interface IBinder
    {
        void Bind(IDataContext context);
        void Unbind();
    }
}