using System;

namespace UIDataBind.Converters
{
    [AttributeUsage(AttributeTargets.Struct)]
    public class ConversionRegistratorAttribute : Attribute
    {
        public Type RegistratorType { get; }
        public ConversionRegistratorAttribute(Type type) =>
            RegistratorType = type;

    }
}