using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore
{
    /// <summary>
    /// The example of initialization and binding <see cref="IBinder"/> to <see cref="IDataContext"/> property.
    /// </summary>
    /// <typeparam name="TValue">
    /// Type of <see cref="Value"/>.
    /// It must be the same or convertible to property type in bound <see cref="IDataContext"/>
    /// </typeparam>
    public class SamplePropertyBinder<TValue> : IBinder
    {
        private readonly string _path;
        private IBindProperty<TValue> _property;

        public bool HasProperty=> _property != null;

        /// <summary>
        /// Accessor to a property value
        /// </summary>
        public TValue Value
        {
            get => _property.Value;
            set => _property.Value = value;
        }

        public IDataContext Context { get; }
        public SamplePropertyBinder(IDataContext context, string path)
        {
            _path = path;
            Context = context;
        }

        /// <summary>
        /// Finding of a property in a <see cref="IDataContext"/> and save it to binder
        /// </summary>
        public void Bind() => _property = Context.FindProperty<TValue>(_path);

        /// <summary>
        /// Remove a property from binder
        /// </summary>
        public void Unbind() => _property = null;
    }
}