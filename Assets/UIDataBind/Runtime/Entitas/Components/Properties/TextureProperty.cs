using Entitas;
using UIDataBind.Base.Components;
using UnityEngine;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct TextureProperty: IPropertyComponent<Texture>, IComponent
    {
        public Texture Value { get; set; }
        public override string ToString() => $"Texture({Value})";
    }
}