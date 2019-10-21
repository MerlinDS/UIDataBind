namespace UIDataBind.Base
{
    public interface IProperties: IEngineProvider
    {
        BindingPath ModelPath { get; }
        RefreshType RefreshType { get; set; }
        BindingPath[] Filter { get; set; }
    }
}