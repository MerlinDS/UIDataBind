using System;

namespace Plugins.UIDataBind.Attributes
{
    public class BaseBindingAttribute : Attribute
    {
        public string Name { get; set; }
        public string BindingName { get; set; }

        public string Help { get; set; }

        protected BaseBindingAttribute(string bindingName, string help = default)
        {
            BindingName = bindingName;
            Help = help;
        }
    }
}