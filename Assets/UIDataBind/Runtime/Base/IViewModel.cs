namespace UIDataBind.Base
{
    /// <summary>
    /// Wrapper for quick access for data
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Update model with existing data
        /// </summary>
        /// <param name="properties"></param>
        void Update(IProperties properties);

        /// <summary>
        /// Fetch data to view
        /// </summary>
        /// <param name="properties"></param>
        void Fetch(IProperties properties);
    }
}