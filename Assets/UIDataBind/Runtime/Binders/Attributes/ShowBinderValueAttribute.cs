using System;
using System.Linq;

namespace UIDataBind.Binders.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShowBinderValueAttribute : Attribute
    {
        private readonly BindingType[] _when;

        public ShowBinderValueAttribute(params BindingType[] when)
        {
            if (when.Length == 0)
                when = (BindingType[]) Enum.GetValues(typeof(BindingType));
            _when = when;
        }

        public bool Contains(BindingType bindingType)
        {
            return _when.Contains(bindingType);
        }
    }
}