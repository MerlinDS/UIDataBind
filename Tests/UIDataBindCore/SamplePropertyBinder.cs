using UIDataBindCore;
using UIDataBindCore.Extensions;

namespace Tests.UIDataBindCore
{
    public class SamplePropertyBinder<TValue> : IBinder
    {
        private readonly string _path;
        private IBindProperty<TValue> _property;

        public bool HasProperty=> _property != null;
        public TValue Value
        {
            get => _property.Value;
            set => _property.Value = value;
        }
        public SamplePropertyBinder(string path) => _path = path;

        public void Bind(IDataContext context) => _property = context.FindProperty<TValue>(_path);

        public void Unbind() => _property = null;
    }
}