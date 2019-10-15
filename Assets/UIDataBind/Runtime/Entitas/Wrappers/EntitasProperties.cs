using UIDataBind.Base;

namespace UIDataBind.Entitas.Wrappers
{
    public struct EntitasProperties : IProperties
    {
        public BindingPath ModelPath { get; }

        public EntitasProperties(BindingPath modelPath) =>
            ModelPath = modelPath;
    }
}