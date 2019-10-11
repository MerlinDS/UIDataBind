using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BindingPathComponent : IComponent
    {
        [EntityIndex]
        public string Value;

        public override string ToString() => $"Path({Value})";
    }
}