using System;

namespace Plugins.UIDataBind.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindingPropertyAttribute : Attribute
    {
        private readonly string _bindingName;
        private readonly bool _allowConversation;

        public bool AllowConversation => _allowConversation;

        public string Name { get; set; }
        public string BindingName => _bindingName;


        public BindingPropertyAttribute(string bindingName = default, bool allowConversation = true)
        {
            _bindingName = bindingName;
            _allowConversation = allowConversation;
        }

    }
}