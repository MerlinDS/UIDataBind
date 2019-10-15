using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct IntProperty : IPropertyComponent<int>, IComponent
    {
        public int Value { get; set; }

        public override string ToString() => $"Int({Value})";
    }
}