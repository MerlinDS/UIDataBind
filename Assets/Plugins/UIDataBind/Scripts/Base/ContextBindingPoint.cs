namespace Plugins.UIDataBind.Base
{
    internal struct ContextBindingPoint
    {
        public IViewContext Context;
        public InstancePoint[] Properties;
        public InstancePoint[] Methods;
        public bool IsEmpty => Context == null;
    }

    internal struct InstancePoint
    {
        public readonly string Name;
        public readonly object Instance;

        public InstancePoint(string name, object instance)
        {
            Name = name;
            Instance = instance;
        }
    }
}