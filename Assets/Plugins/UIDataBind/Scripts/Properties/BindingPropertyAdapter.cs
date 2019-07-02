using System;
using Plugins.UIDataBind.Utils;

namespace Plugins.UIDataBind.Properties
{
    public sealed class BindingPropertyAdapter<TValue> : IBindingProperty<TValue>
    {
        private readonly IBindingProperty _source;
        private readonly IBindingProperty<TValue> _target;
        private bool _upToDate;

        public BindingPropertyAdapter(IBindingProperty source) :
            this(source, new BindingProperty<TValue>())
        {
        }

        private BindingPropertyAdapter(IBindingProperty source, IBindingProperty<TValue> target)
        {
            _source = source;
            _target = target;

            ((BindingProperty)_source).OnInternalUpdateValue += SourceValueHandler;
            SourceValueHandler();
        }

        ~BindingPropertyAdapter()
        {
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            if(_source != null)
                ((BindingProperty)_source).OnInternalUpdateValue -= SourceValueHandler;
            _upToDate = false;
        }

        private void SourceValueHandler()
        {
            if(!_upToDate)
                _target.Value = ConvertUtils.ConvertTo<TValue>(_source.Value);
        }

        public event Action<TValue> OnUpdateValue
        {
            add => _target.OnUpdateValue += value;
            remove => _target.OnUpdateValue -= value;
        }

        public Type GetValueType => _target.GetValueType;

        public TValue Value
        {
            get => _target.Value;
            set
            {
                try
                {
                    _upToDate = true;
                    _target.Value = value;
                    _source.Value = ConvertUtils.ConvertTo(_source.Value.GetType(), value);
                    _upToDate = false;
                }
#pragma warning disable 168
                catch (FormatException exception)
#pragma warning restore 168
                {
                    _upToDate = false;
                    SourceValueHandler();
                }
            }
        }

        object IBindingProperty.Value
        {
            get => Value;
            set => Value = (TValue) value;
        }
    }
}