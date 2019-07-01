using System;
using JetBrains.Annotations;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Extensions;
using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public abstract class BasePropertyBinding<TValue> : BaseBinding, IPropertyBindingBehaviour<TValue>
    {
        private IBindingProperty<TValue> _bindingProperty;

#pragma warning disable 0649
        [HideInInspector]
        [SerializeField]
        private TValue _value;
#pragma warning restore 0649

        [UsedImplicitly]
        // ReSharper disable once ConvertToAutoProperty
        protected virtual TValue DefaultValue => _value;

        public TValue Value
        {
            get => _bindingProperty != null ? _bindingProperty.Value : _value;
            set
            {
                if (_bindingProperty != null)
                    _bindingProperty.Value = value;
                _value = value;
            }
        }

        public Type GetValueType => typeof(TValue);


        protected sealed override void Activate(BindingPath path)
        {
            _bindingProperty?.Dispose();
            _bindingProperty = this.FindBindingProperty(path, DefaultValue);
            if (_bindingProperty == null)
            {
                var contextName = this.FindContextName();
                Debug.LogWarning(
                    $"Property {path.Name} was not founds in {contextName}! Insure that context was added " +
                    $"and has {path.Name} readonly field.");
                enabled = false;
                return;
            }

            _bindingProperty.OnUpdateValue += UpdateValueHandler;
            UpdateValueHandler(_bindingProperty.Value);
        }

        protected sealed override void Deactivate()
        {
            if (_bindingProperty == null)
                return;

            _bindingProperty.OnUpdateValue -= UpdateValueHandler;
            _bindingProperty.Dispose();
        }

        protected abstract void UpdateValueHandler(TValue value);

    }
}