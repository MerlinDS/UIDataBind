using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct FloatPropertyComponent: IPropertyComponent<float>, IComponent
    {
        public float Value { get; set; }
        public override string ToString() => $"Float({Value})";
    }
}