using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct ParentModel: IBindingPathProvider, IComponent
    {
        public Base.BindingPath Path { get; set; }

        public override string ToString() => $"Parent{Path}";
    }
}