using System;
using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Converters;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind, ConversionRegistrator(typeof(Registrator))]
    public struct IntProperty : IPropertyComponent<int>, IComponent
    {
        public int Value { get; set; }

        public override string ToString() => $"Int({Value})";

        private struct Registrator : IConversionRegistrator
        {
            public void Register(IConverters converters)
            {
                converters.Register((int x)=>Convert.ToBoolean(x));
                converters.Register((int x)=>Convert.ToSingle(x));
                converters.Register((int x)=>Convert.ToString(x));
            }
        }
    }
}