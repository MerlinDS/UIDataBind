namespace UIDataBind.Base
{
    public interface IProperties
    {
        void Refresh<TValue>(BindingPath propertyName, ref TValue value);
    }
}