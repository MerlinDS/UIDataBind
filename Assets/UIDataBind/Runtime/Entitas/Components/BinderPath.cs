using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BinderPath : IComponent
    {
        [EntityIndex]
        public Base.BindingPath Value;

        public override string ToString() => Value;
    }
}