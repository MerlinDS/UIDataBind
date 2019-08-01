namespace UIDataBindCore.Converters
{
    public interface IPropertyConverter<TValue0, TValue1> : IPropertyConverter
    {
        TValue0 Convert(TValue1 value);
        TValue1 Convert(TValue0 value);
    }

    /// <summary>
    /// Converting <see cref="IBindProperty"/> value of source type to <see cref="IBindProperty"/> value of target type
    /// </summary>
    public interface IPropertyConverter
    {

    }
}