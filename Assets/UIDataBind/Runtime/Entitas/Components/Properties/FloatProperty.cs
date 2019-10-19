using System;
using System.Globalization;
using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Converters;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind, ConversionRegistrator(typeof(Registrator))]
    public struct FloatProperty: IPropertyComponent<float>, IComponent
    {
        public float Value { get; set; }
        public override string ToString() => $"Float({Value})";

        private struct Registrator : IConversionRegistrator
        {
            public void Register(IConverters converters)
            {
                converters.Register((float x)=>Convert.ToBoolean(x));
                converters.Register((float x)=>Convert.ToInt32(x));
                converters.Register((float x)=>Convert.ToString(x, CultureInfo.InvariantCulture));
            }
        }
    }
}