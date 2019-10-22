using Entitas;
using Entitas.CodeGeneration.Attributes;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct ModelPath : IComponent
    {
        [PrimaryEntityIndex]
        public Base.OldBindingPath Value;

        public override string ToString() => Value;
    }
}