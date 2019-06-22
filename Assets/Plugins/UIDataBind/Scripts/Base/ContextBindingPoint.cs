using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Base
{
    internal struct ContextBindingPoint
    {
        public IViewContext Context;
        public PropertyPoint[] Properties;
        public bool IsEmpty => Context == null;
    }

    internal struct PropertyPoint
    {
        public string Name;
        public IBindingProperty Instance;

        public PropertyPoint(string name, IBindingProperty instance)
        {
            Name = name;
            Instance = instance;
        }
    }
}