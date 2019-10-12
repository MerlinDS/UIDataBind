using Entitas;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct ModelComponent : IComponent
    {
        public override string ToString() => "Model";
    }
}