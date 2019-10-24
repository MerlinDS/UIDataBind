using System;
using Entitas;
using UIDataBind.Base;
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

        public void Initialize() =>
            _converters.Register(_componentTypes);
    }
}