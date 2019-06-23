namespace Plugins.UIDataBind.Base
{
    public interface IPropertyBindingBehaviour<TValue>
    {
        TValue Value { get; set; }
    }
}