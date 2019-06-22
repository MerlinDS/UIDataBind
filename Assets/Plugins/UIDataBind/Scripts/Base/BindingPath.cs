using System;

namespace Plugins.UIDataBind.Base
{
    [Serializable]
    public struct BindingPath
    {
        public string PropertyName;
        public BaseView View;
        public Type ViewType;
    }
}