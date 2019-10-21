using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Base
{
    /// <summary>
    /// Wrapper for quick access for data
    /// </summary>
    public interface IViewModel
    {
        void Refresh(RefreshType actionType, IProperties properties);
    }
}