using System;

namespace Plugins.UIDataBind.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class BindingPropertyAttribute : BaseBindingAttribute
    {
        private readonly bool _allowConversation;

        public bool AllowConversation => _allowConversation;


        public BindingPropertyAttribute(string bindingName = default, bool allowConversation = true)
            : base(bindingName) => _allowConversation = allowConversation;
    }
}