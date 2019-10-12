using System;
using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct BooleanPropertyComponent : IPropertyComponent<bool>, IComponent
    {
        public bool Value { get; set; }
        public override string ToString() => $"Boolean({Value})";
    }
}