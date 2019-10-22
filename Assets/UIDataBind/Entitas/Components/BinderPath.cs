using Entitas;
using Entitas.CodeGeneration.Attributes;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Base.Extensions;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BinderPath : IPathComponent, IComponent
    {
        [EntityIndex]
        public BindingPath Value { get; set; }

        public override string ToString() => this.ToHumanString();
    }
}