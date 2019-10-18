using Entitas;
using UIDataBind.Base.Components;
using UnityEngine;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct SpriteProperty: IPropertyComponent<Sprite>, IComponent
    {
        public Sprite Value { get; set; }
        public override string ToString() => $"Sprite({Value})";
    }
}