namespace Plugins.UIDataBind.Components
{
    public interface IPropertyBindingBehaviour<TValue>
    {
        TValue Value { get; set; }
    }
}