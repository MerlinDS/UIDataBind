namespace UIDataBindCore
{
    /// <summary>
    /// The main interface of the data context.
    /// A class must inherit this interface to be marked as data context and be registered in UIDataBind
    /// </summary>
    public interface IDataContext
    {

    }

    /// <summary>
    /// Add this interface for context auto initialization
    /// </summary>
    public interface IInitializable
    {
        void Init();
    }
}