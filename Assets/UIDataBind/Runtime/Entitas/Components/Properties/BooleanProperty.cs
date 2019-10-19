using System;
using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Converters;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind, ConversionRegistrator(typeof(Registrator))]
    public struct BooleanProperty : IPropertyComponent<bool>, IComponent
    {
        public bool Value { get; set; }

        public override string ToString() => $"Boolean({Value})";

        private struct Registrator : IConversionRegistrator
        {
            public void Register(IConverters converters)
            {
                converters.Register((bool x)=>Convert.ToInt32(x));
                converters.Register((bool x)=>Convert.ToSingle(x));
                converters.Register((bool x)=>Convert.ToString(x));
            }
        }
    }
}