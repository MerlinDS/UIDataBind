using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public class ControlEventProperty : IPropertyComponent<ControlEvent>, IComponent
    {
        public ControlEvent Value { get; set; }

        public override string ToString() => $"ControlEvent({Value})";
    }
}