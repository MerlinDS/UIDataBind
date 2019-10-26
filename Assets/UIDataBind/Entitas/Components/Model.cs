using Entitas;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct Model : IComponent
    {
        public override string ToString() => nameof(Model);
    }
}