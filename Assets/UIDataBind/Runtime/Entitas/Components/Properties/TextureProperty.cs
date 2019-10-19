using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Converters;
using UIDataBind.Utils;
using UnityEngine;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind, ConversionRegistrator(typeof(Registrator))]
    public struct TextureProperty : IPropertyComponent<Texture>, IComponent
    {
        public Texture Value { get; set; }
        public override string ToString() => $"Texture({Value})";

        private struct Registrator : IConversionRegistrator
        {
            public void Register(IConverters converters)
            {
                converters.Register((string x) => Resources.Load<Texture>(x));
                converters.Register((Texture x) => x.GetName());
            }
        }
    }
}