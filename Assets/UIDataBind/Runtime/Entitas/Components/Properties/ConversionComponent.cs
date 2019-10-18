using System;
using Entitas;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public class ConvertTo : IComponent
    {
        public Type Value;

        public override string ToString() => $"Convert to ({Value})";
    }

    [UiBind]
    public class Converted : IComponent
    {
        public object Value;

        public override string ToString() => $"Converted Value ({Value})";
    }
}