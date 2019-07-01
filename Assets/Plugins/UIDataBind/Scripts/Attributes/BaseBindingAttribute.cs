using System;

namespace Plugins.UIDataBind.Attributes
{
    public class BaseBindingAttribute : Attribute
    {
        public string Name { get; set; }
        public string BindingName { get; set; }

        protected BaseBindingAttribute(string bindingName)
        {
            BindingName = bindingName;
        }
    }
}