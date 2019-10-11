using Entitas;

namespace UIDataBind.Entitas.Components
{
    /// <summary>
    /// Update is needed
    /// </summary>
    [UiBind]
    public struct DirtyComponent : IComponent
    {
        public override string ToString() => "Dirty";
    }
}