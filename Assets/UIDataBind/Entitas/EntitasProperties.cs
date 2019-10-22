using UIDataBind.Base;

namespace UIDataBind.Entitas
{
    public struct EntitasProperties : IProperties
    {
        public OldBindingPath ModelPath { get; }
        public RefreshType RefreshType { get; set; }
        public OldBindingPath[] Filter { get; set; }

        public EntitasProperties(OldBindingPath modelPath)
        {
            ModelPath = modelPath;
            RefreshType = RefreshType.None;
            Filter = default;
        }
    }
}