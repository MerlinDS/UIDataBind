using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;
using UIDataBindCore.Properties;

namespace Tests.UIDataBindCore
{
    public class SampleDataContext : IDataContext, IBinder
    {
        public SampleDataContext() => Bind(this);
        public void Bind(IDataContext context) => context.Register();
        public bool MethodInvoked;

        public void Unbind()
        {
            BooleanProperty.Dispose();
            this.Unregister();
        }

        [Bind]
        public void BindMethod() => MethodInvoked = true;

        [Bind]
        public readonly BindProperty<bool> BooleanProperty = new BindProperty<bool>(true);

        public bool Boolean
        {
            get => BooleanProperty.Value;
            set => BooleanProperty.Value = value;
        }

        [Bind]
        public readonly BindProperty<int> IntProperty = new BindProperty<int>(1);

        public int Int32
        {
            get => IntProperty.Value;
            set => IntProperty.Value = value;
        }
    }
}