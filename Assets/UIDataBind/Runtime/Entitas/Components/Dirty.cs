using Entitas;

namespace UIDataBind.Entitas.Components
{
    /// <summary>
    /// Update is needed
    /// </summary>
    [UiBind]
    public struct Dirty : IComponent
    {
        public override string ToString() => "Dirty";
    }
}