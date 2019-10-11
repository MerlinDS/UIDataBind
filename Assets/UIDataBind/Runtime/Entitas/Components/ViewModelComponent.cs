using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct ViewModelComponent : IComponent
    {
        [PrimaryEntityIndex]
        public Guid Id;

        public override string ToString() => $"ViewModel({Id})";
    }
}