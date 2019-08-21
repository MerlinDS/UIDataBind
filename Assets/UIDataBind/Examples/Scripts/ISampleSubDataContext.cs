using UIDataBindCore;

namespace UIDataBind.Examples
{
    public interface ISampleSubDataContext : IDataContext
    {
        bool Visible { get; set; }
        string Label { get; }
    }
}