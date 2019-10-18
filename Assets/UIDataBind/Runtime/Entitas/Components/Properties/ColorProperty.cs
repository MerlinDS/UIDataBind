using Entitas;
using UIDataBind.Base.Components;
using UnityEngine;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct ColorProperty: IPropertyComponent<Color>, IComponent
    {
        public Color Value { get; set; }
        public override string ToString() => $"Color({Value})";
    }
}