using System;

namespace Plugins.UIDataBind.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BindingMethodAttribute : BaseBindingAttribute
    {
        public BindingMethodAttribute() : base(default)
        {
        }

        public BindingMethodAttribute(string bindingName = default) : base(bindingName)
        {
        }
    }
}