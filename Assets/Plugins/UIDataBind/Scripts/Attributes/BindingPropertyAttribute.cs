using System;

namespace Plugins.UIDataBind.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindingPropertyAttribute : BaseBindingAttribute
    {
        public BindingPropertyAttribute() : this(default)
        {

        }

        public BindingPropertyAttribute(string bindingName = default)
            : base(bindingName)
        {
        }
    }
}