using System;
using Entitas;
using UIDataBind.Base.Components;
using UIDataBind.Converters;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind, ConversionRegistrator(typeof(Registrator))]
    public struct StringProperty: IPropertyComponent<string>, IComponent
    {
        public string Value { get; set; }
        public override string ToString() => $"Str({Value})";

        private struct Registrator : IConversionRegistrator
        {
            public void Register(IConverters converters)
            {
                converters.Register((string x)=>Convert.ToSingle(x));
                converters.Register((string x)=>Convert.ToInt32(x));
                converters.Register((string x)=>Convert.ToBoolean(x));
            }
        }
    }
}