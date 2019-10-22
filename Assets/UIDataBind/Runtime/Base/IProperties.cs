namespace UIDataBind.Base
{
    public interface IProperties: IEngineProvider
    {
        OldBindingPath ModelPath { get; }
        RefreshType RefreshType { get; set; }
        OldBindingPath[] Filter { get; set; }
    }
}