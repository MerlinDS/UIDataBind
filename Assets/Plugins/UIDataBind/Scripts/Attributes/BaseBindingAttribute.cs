using System;

namespace Plugins.UIDataBind.Attributes
{
    public class BaseBindingAttribute : Attribute
    {
        private readonly string _bindingName;

        public string Name { get; set; }
        public string BindingName => _bindingName;

        protected BaseBindingAttribute(string bindingName)
        {
            _bindingName = bindingName;
        }
    }
}