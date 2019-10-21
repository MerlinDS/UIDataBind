namespace UIDataBind.Base
{
    public interface IProperties: IEngineProvider
    {
        RefreshType RefreshType { get; set; }
        BindingPath ModelPath { get; }
    }
}