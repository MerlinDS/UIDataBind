using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct PathComponent : IComponent
    {
        [PrimaryEntityIndex]
        public string Value;

        public override string ToString() => $"Path({Value})";
    }
}