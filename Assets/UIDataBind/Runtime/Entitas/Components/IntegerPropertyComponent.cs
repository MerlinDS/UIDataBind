using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct IntegerPropertyComponent : IPropertyComponents<int>, IComponent
    {
        public int Value { get; set; }

        public override string ToString() => $"Int({Value})";
    }
}