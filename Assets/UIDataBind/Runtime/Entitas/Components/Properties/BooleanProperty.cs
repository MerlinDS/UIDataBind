using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct BooleanProperty : IPropertyComponent<bool>, IComponent
    {
        public bool Value { get; set; }

        public override string ToString() => $"Boolean({Value})";
    }
}