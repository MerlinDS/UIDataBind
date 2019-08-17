using UIDataBindCore;
using UIDataBindCore.Extensions;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public abstract class PropertyBinder<TValue> : BaseBinder
    {
        private IBindProperty<TValue> _property;

        #region Serialized fields

#pragma warning disable 0649
        [SerializeField]
        private TValue _value;

#pragma warning restore 0649

        #endregion

        public TValue Value
        {
            get => _property != null ? _property.Value : _value;
            set
            {
                if (_property != null)
                    _property.Value = value;
                _value = value;
            }
        }

        public override void Bind()
        {
            _property?.Dispose();
            _property = Context.FindProperty<TValue>(Path);
            if (_property == null)
            {
                Debug.LogWarning(
                    $"Property {Path} was not founds in {Context}! Insure that context was added " +
                    $"and has {Path} readonly field.");
                enabled = false;
                return;
            }

            _property.OnUpdate += UpdateValueHandler;
            UpdateValueHandler(_property.Value);
        }

        public override void Unbind()
        {
            if (_property == null)
                return;

            _property.OnUpdate -= UpdateValueHandler;
            _property.Dispose();
        }

        protected abstract void UpdateValueHandler(TValue value);
    }
}