using UIDataBindCore;

namespace Plugins.UIDataBind.Examples
{
    public interface IVisibleDataContext : IDataContext
    {
        bool Visible { get; set; }
    }
}