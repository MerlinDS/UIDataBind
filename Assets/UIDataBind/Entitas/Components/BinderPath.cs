using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BinderPath : IComponent
    {
        [EntityIndex]
        public Base.OldBindingPath Value;

        public override string ToString() => Value;
    }
}