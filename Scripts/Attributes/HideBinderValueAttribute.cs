using System;
using System.Linq;
using Plugins.UIDataBind.Binders;

namespace Plugins.UIDataBind.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HideBinderValueAttribute : Attribute
    {
        private readonly BindingType[] _when;

        public HideBinderValueAttribute(params BindingType[] when)
        {
            if (when.Length == 0)
                when = (BindingType[])Enum.GetValues(typeof(BindingType));
            _when = when;
        }

        public bool Contains(BindingType bindingType) =>
            _when.Contains(bindingType);
    }
}