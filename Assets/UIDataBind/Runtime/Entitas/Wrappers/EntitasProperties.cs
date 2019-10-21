using UIDataBind.Base;

namespace UIDataBind.Entitas.Wrappers
{
    public struct EntitasProperties : IProperties
    {
        public BindingPath ModelPath { get; }
        public RefreshType RefreshType { get; set; }
        public BindingPath[] Filter { get; set; }

        public EntitasProperties(BindingPath modelPath)
        {
            ModelPath = modelPath;
            RefreshType = RefreshType.None;
            Filter = default;
        }
    }
}