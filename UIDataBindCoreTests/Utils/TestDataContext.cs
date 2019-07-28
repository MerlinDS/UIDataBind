using UIDataBindCore;
using UIDataBindCore.Attributes;

namespace UIDataBindCoreTests.Utils
{
    public class TestDataContext : IDataContext
    {
        public const string BindMemberAlias = nameof(BindMemberAlias);
        public const string BindMemberHelp = nameof(BindMemberHelp);

        public const string BindMethodAlias = nameof(BindMethodAlias);
        public const string BindMethodHelp = nameof(BindMethodHelp);

        public bool IsBindMethodInvoked;

        [Bind(BindMemberAlias, BindMemberHelp)]
        public readonly IBindProperty BindMember = new TestBindProperty();

        [Bind(BindMethodAlias, BindMethodHelp)]
        public void BindMethod()
        {
            IsBindMethodInvoked = true;
        }
    }

    public class TestBindProperty : IBindProperty
    {
        public void Dispose()
        {
        }
    }
}