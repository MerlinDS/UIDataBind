using System;

namespace Plugins.UIDataBind.Base
{
    [Serializable]
    public struct BindingPath
    {
        public BindingType Type;
        public string PropertyName;
    }
}