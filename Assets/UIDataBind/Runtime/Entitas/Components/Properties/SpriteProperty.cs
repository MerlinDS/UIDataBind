using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Converters;
using UIDataBind.Utils;
using UnityEngine;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind, ConversionRegistrator(typeof(Registrator))]
    public struct SpriteProperty : IPropertyComponent<Sprite>, IComponent
    {
        public Sprite Value { get; set; }
        public override string ToString() => $"Sprite({Value})";

        private struct Registrator : IConversionRegistrator
        {
            public void Register(IConverters converters)
            {
                converters.Register((string x) => Resources.Load<Sprite>(x));
                converters.Register((Sprite x) => x.GetName());
            }
        }
    }
}