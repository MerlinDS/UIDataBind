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

        [Bind(BindMemberAlias, BindMemberHelp)]
        public readonly int BindMember = 1;

        [Bind(BindMethodAlias, BindMethodHelp)]
        public void BindMethod()
        {
        }
    }
}