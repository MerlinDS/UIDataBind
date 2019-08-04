using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;
using UIDataBindCore.Properties;

namespace Tests.UIDataBindCore
{
    /// <summary>
    /// The example of initialization and binding <see cref="IDataContext"/> to external sources.
    /// In this case it will register to kernel by itself.
    /// But you could register <see cref="IDataContext"/> in external classes as well.
    /// </summary>
    public class SampleDataContext : IDataContext, IBinder
    {
        public SampleDataContext() => Bind(this);

        /// <summary>
        /// A DataContext needs to be registered before binding it to external sources.
        /// It could be done from a constructor or other initialization methods.
        /// </summary>
        public void Bind(IDataContext context)
            => context.Register();

        /// <summary>
        /// Before disposing of a DataContext use unregister to remove it from binding.
        /// It could be done from a destructor or disposing methods (IDisposable.Dispose for example).
        /// </summary>
        public void Unbind()
        {
            /*
             * Disposing properties.
             * It's not necessary in most cases. Yet, it could be done here, to remove listeners for example.
             */
            IntProperty.Dispose();
            BooleanProperty.Dispose();
            //Unregistering from binding kernel
            this.Unregister();
        }

        #region Binded Methods

        /// <summary>
        /// Will change a <see cref="Boolean"/> field on invocation.
        /// <remarks>
        /// Only methods without arguments and the return type could be bonded.
        /// </remarks>
        /// </summary>
        [Bind]
        public void BindMethod() => Boolean = true;

        #endregion

        #region Bindede properties

        /// <summary>
        /// Boolean property that will be bind to external sources
        /// </summary>
        [Bind]
        public readonly BooleanProperty BooleanProperty = new BooleanProperty(true);

        /// <summary>
        /// Accessor to the <see cref="BooleanProperty"/> value
        /// </summary>
        public bool Boolean
        {
            get => BooleanProperty.Value;
            set => BooleanProperty.Value = value;
        }

        /// <summary>
        /// Int property that will be bind to external sources
        /// </summary>
        [Bind]
        public readonly IntProperty IntProperty = new IntProperty(1);

        /// <summary>
        /// Accessor to the <see cref="IntProperty"/> value
        /// </summary>
        public int Int32
        {
            get => IntProperty.Value;
            set => IntProperty.Value = value;
        }

        #endregion
    }
}