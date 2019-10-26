
using Entitas;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct Initialized : IComponent
    {
        public override string ToString() => nameof(Initialized);
    }
}