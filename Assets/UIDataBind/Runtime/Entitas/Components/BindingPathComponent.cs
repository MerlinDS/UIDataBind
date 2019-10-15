using Entitas;
using Entitas.CodeGeneration.Attributes;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BindingPathComponent : IComponent
    {
        [EntityIndex]
        public BindingPath Value;

        public override string ToString() => Value;
    }
}