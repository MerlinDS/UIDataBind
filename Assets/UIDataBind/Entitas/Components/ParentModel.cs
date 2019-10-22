using System;
using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind][Obsolete("Use BindingPath to get parent path")]
    public struct ParentModel: IBindingPathProvider, IComponent
    {
        public BindingPath Path { get; set; }

        public override string ToString() => $"Parent{Path}";
    }
}