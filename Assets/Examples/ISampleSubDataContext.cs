using UIDataBindCore;

namespace Plugins.UIDataBind.Examples
{
    public interface ISampleSubDataContext : IDataContext
    {
        bool Visible { get; set; }
        string Label { get; }
    }
}