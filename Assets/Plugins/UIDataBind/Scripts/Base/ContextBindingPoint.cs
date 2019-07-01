using System;

namespace Plugins.UIDataBind.Base
{
    internal struct ContextBindingPoint
    {
        public IViewContext Context;
        public Tuple<string, object>[] Properties;
        public Tuple<string, object>[] Methods;
        public bool IsEmpty => Context == null;
    }
}