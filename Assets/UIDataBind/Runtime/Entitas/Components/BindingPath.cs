using Entitas;
using Entitas.CodeGeneration.Attributes;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BindingPath : IComponent
    {
        [EntityIndex]
        public Base.BindingPath Value;

        public override string ToString() => Value;
    }
}