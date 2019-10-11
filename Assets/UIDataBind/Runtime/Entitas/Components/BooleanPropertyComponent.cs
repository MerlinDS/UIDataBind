using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct BooleanPropertyComponent : IPropertyComponents<bool>, IComponent
    {
        public bool Value { get; set; }

        public override string ToString() => $"Boolean({Value})";
    }
}