using System;
using JetBrains.Annotations;
using UIDataBindCore;
using UIDataBindCore.Extensions;
using UIDataBindCore.Properties;
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

        [UsedImplicitly]
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

        #region Bindings

        public override void Bind()
        {
            Unbind();

            switch (BindingType)
            {
                case BindingType.None:
                    BindToSelf();
                    break;
                case BindingType.Context:
                    BindToContext();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BindToSelf()
        {
            if (BindingType != BindingType.None)
                return;

            _property = new BindProperty<TValue>(_value);
            InternalUpdateValueHandler(_property.Value);
        }

        private void BindToContext()
        {
            if (BindingType != BindingType.Context)
                return;

            _property = Context.FindProperty<TValue>(Path);
            if (_property == null)
            {
                Debug.LogWarning(
                    $"Property {Path} was not founds in {Context}! Insure that context was added " +
                    $"and has {Path} readonly field.");
                enabled = false;
                return;
            }

            _property.OnUpdate += InternalUpdateValueHandler;
            InternalUpdateValueHandler(_property.Value);
        }

        public override void Unbind()
        {
            if (_property == null)
                return;

            _property.OnUpdate -= InternalUpdateValueHandler;
            _property.Dispose();
        }

        #endregion

        private void InternalUpdateValueHandler(TValue value)
        {
            _value = value;
            UpdateValueHandler(value);
        }
        protected abstract void UpdateValueHandler(TValue value);
    }
}