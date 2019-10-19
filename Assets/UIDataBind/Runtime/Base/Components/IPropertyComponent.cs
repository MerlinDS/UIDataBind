using UIDataBind.Converters;

namespace UIDataBind.Base.Components
{
    /// <summary>
    /// A component that contains value of a property
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IPropertyComponent<TValue> : IPropertyComponent
    {
        TValue Value { get; set; }
    }

    public interface IPropertyComponent
    {
    }

    public interface IConversionRegistrator
    {
        void Register(IConverters converters);
    }
}