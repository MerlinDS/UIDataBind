namespace UIDataBind.Base
{
    public interface IProperties: IEngineProvider
    {
        BindingPath ModelPath { get; }
    }
}