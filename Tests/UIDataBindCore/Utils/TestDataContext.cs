using System;
using NSubstitute;
using UIDataBindCore;
using UIDataBindCore.Attributes;

namespace Tests.UIDataBindCore.Utils
{
    public class TestDataContext : IDataContext
    {
        public const string BindMemberAlias = nameof(BindMemberAlias);
        public const string BindMemberHelp = nameof(BindMemberHelp);

        public const string BindMethodAlias = nameof(BindMethodAlias);
        public const string BindMethodHelp = nameof(BindMethodHelp);

        public bool IsBindMethodInvoked;

        [Bind(BindMemberAlias, BindMemberHelp)]
        public readonly IBindProperty BindMember = Substitute.For<IBindProperty>();

        [Bind(BindMethodAlias, BindMethodHelp)]
        public void BindMethod()
        {
            IsBindMethodInvoked = true;
        }

        [Bind(BindMethodAlias, BindMethodHelp)]
        public void BindMethodWithArgs(int value)
        {
            Property = value;
        }

        [PropertyBind]
        public int Property { get; set; }

        [AttributeUsage(AttributeTargets.Property)]
        private class PropertyBindAttribute : BindAttribute
        {

        }
    }
}