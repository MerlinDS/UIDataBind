namespace UIDataBind.Base.Components
{
    public interface IPropertyComponent<TValue> : IPropertyComponent
    {
        TValue Value { get; set; }
    }

    public interface IPropertyComponent{}
}