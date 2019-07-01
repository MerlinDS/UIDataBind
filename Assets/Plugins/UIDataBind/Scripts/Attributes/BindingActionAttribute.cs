using System;

namespace Plugins.UIDataBind.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BindingActionAttribute : BaseBindingAttribute
    {
        public BindingActionAttribute(string bindingName = default) : base(bindingName)
        {
        }
    }
}