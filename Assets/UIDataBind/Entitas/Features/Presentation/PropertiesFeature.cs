using System;
using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Runtime.Base.Extensions;

namespace UIDataBind.Entitas.Features.Presentation
{
    public sealed class PropertiesFeature : Feature
    {
        public PropertiesFeature(UiBindContext context)
        {

            Add(new InitConvertersSystem(context.GetEngine()));
            AddPropertyUpdateSystems(context);

        }

        private void AddPropertyUpdateSystems(IEngineProvider context)
        {
            var systemType = typeof(PropertyUpdateSystem<>);
            foreach (var propertyType in context.GetEngine().PropertyTypes)
                Add((ISystem) Activator.CreateInstance(systemType.MakeGenericType(propertyType), context));
        }
    }
}