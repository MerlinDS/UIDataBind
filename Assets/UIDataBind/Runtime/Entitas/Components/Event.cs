using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct Event : IComponent
    {
        public ControlEvent Value;

        public override string ToString() => $"Event({Value})";
    }
}