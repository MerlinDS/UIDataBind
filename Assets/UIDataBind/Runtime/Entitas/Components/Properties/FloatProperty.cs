using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct FloatProperty: IPropertyComponent<float>, IComponent
    {
        public float Value { get; set; }
        public override string ToString() => $"Float({Value})";
    }
}