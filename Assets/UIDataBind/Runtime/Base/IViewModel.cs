namespace UIDataBind.Base
{
    /// <summary>
    /// Wrapper for quick access for data
    /// </summary>
    public interface IViewModel
    {
        void Refresh(ModelQuery query);
    }
}