using Entitas;
using Entitas.CodeGeneration.Attributes;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct ModelPathComponent : IComponent
    {
        [PrimaryEntityIndex]
        public BindingPath Value;

        public override string ToString() => Value;
    }
}