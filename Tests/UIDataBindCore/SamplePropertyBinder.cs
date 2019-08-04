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
        public SamplePropertyBinder(string path) => _path = path;

        /// <summary>
        /// Finding of a property in a <see cref="IDataContext"/> and save it to binder
        /// </summary>
        /// <param name="context">A context instance where a property stored</param>
        public void Bind(IDataContext context) => _property = context.FindProperty<TValue>(_path);

        /// <summary>
        /// Remove a property from binder
        /// </summary>
        public void Unbind() => _property = null;
    }
}