using System;
using UIDataBindCore.Converters;

namespace UIDataBindCore.Properties
{
    public class BindPropertyAdapter<TSource, TTarget> : IBindProperty<TTarget>
    {
        private bool _upToDate;

        private readonly IBindProperty<TSource> _source;
        private readonly IBindProperty<TTarget> _target;

        private readonly IPropertyConverter<TSource, TTarget> _converter;

        #region Public API

        public BindPropertyAdapter(IBindProperty source,
            IPropertyConverter converter)
        {
            _source = (IBindProperty<TSource>) source;
            _converter = (IPropertyConverter<TSource, TTarget>) converter;

            _target = new BindProperty<TTarget>();
            _source.OnUpdate += SourceUpdateHandler;
            SourceUpdateHandler(_source.Value);
        }

        public Type ValueType => _target.ValueType;

        public event Action<TTarget> OnUpdate
        {
            add => _target.OnUpdate += value;
            remove => _target.OnUpdate -= value;
        }

        public TTarget Value
        {
            get => _target.Value;
            set
            {
                _upToDate = true;
                _target.Value = value;
                _source.Value = _converter.Convert(value);
                _upToDate = false;
            }
        }

        public void Dispose() =>
            _source.OnUpdate -= SourceUpdateHandler;

        #endregion

        #region Private methods

        private void SourceUpdateHandler(TSource value)
        {
            if (!_upToDate)
                _target.Value = _converter.Convert(value);
        }

        #endregion
    }
}