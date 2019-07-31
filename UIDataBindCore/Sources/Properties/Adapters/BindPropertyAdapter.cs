using System;

namespace UIDataBindCore.Properties.Adapters
{
    public class BindPropertyAdapter<TSource, TTarget> : IBindProperty<TTarget>
    {
        private bool _upToDate;

        private readonly IBindProperty<TSource> _source;
        private readonly IBindProperty<TTarget> _target;

        private readonly Func<TSource, TTarget> _toTarget;
        private readonly Func<TTarget, TSource> _toSource;


        #region Public API

        public static IBindProperty<TTarget> Instantiate(IBindProperty<TSource> source,
            Func<TTarget, TSource> toSource, Func<TSource, TTarget> toTarget) =>
            new BindPropertyAdapter<TSource, TTarget>(source, toSource, toTarget);

        private BindPropertyAdapter(IBindProperty<TSource> source, Func<TTarget, TSource> toSource,
            Func<TSource, TTarget> toTarget)
        {
            _source = source;
            _toTarget = toTarget;
            _toSource = toSource;

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
                try
                {
                    _upToDate = true;
                    _target.Value = value;
                    _source.Value = ToSource(value);
                    _upToDate = false;
                }
#pragma warning disable 168
                catch (FormatException exception)
#pragma warning restore 168
                {
                    _upToDate = false;
                    SourceUpdateHandler(_source.Value);
                }
            }
        }

        public void Dispose() =>
            _source.OnUpdate -= SourceUpdateHandler;

        #endregion

        #region Private methods

        private void SourceUpdateHandler(TSource value)
        {
            if (!_upToDate)
                _target.Value = ToTarget(value);
        }

        private TSource ToSource(TTarget value)
        {
            try
            {
                return _toSource.Invoke(value);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e);
                return _source.Value;
            }
        }

        private TTarget ToTarget(TSource value)
        {
            try
            {
                return _toTarget.Invoke(value);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e);
                return _target.Value;
            }

        }

        #endregion
    }
}