using System;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Converters;

namespace UIDataBind.Entitas.Features.Presentation
{
    internal sealed class InitConvertersSystem : IInitializeSystem
    {
        private readonly Type[] _componentTypes;
        private readonly IConverters _converters;

        public InitConvertersSystem(IECSEngine engine)
        {
            _componentTypes = engine.ComponentTypes;
            _converters = engine.Converters;
        }

        public void Initialize()
        {
            var attributeType = typeof(ConversionRegistratorAttribute);
            foreach (var componentType in _componentTypes)
            {
                if(!Attribute.IsDefined(componentType, attributeType))
                    continue;

                var attribute = (ConversionRegistratorAttribute)Attribute.GetCustomAttribute(componentType, attributeType);
                var registrator = (IConversionRegistrator)Activator.CreateInstance(attribute.RegistratorType);
                registrator.Register(_converters);
            }
        }
    }
}