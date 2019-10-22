using System;
using UIDataBind.Base;

namespace UIDataBind.Runtime.Base.Extensions
{
    public static class ECSEngineExtension
    {
        private static IECSEngine _engine;

        public static void Register<TEngine>()
            where TEngine : IECSEngine
        {
            if(_engine != null)
                throw new AggregateException("Already registered");
            _engine = Activator.CreateInstance<TEngine>();
        }

        public static IECSEngine GetEngine(this IEngineProvider binder)
        {
            if(_engine == null)
                throw new InvalidOperationException("Engine was not registered yet");
            return _engine;
        }
    }
}