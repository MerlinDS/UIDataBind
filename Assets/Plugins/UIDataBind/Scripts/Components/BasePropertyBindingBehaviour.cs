using JetBrains.Annotations;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Extensions;
using Plugins.UIDataBind.Properties;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public abstract class BasePropertyBindingBehaviour<TValue> : BaseBinding, IPropertyBindingBehaviour<TValue>
    {
        private IBindingProperty<TValue> _bindingProperty;

#pragma warning disable 0649
        [HideInInspector]
        [SerializeField]
        private TValue _value;
#pragma warning restore 0649

        [UsedImplicitly]
        private TValue DefaultValue => _value;

        public TValue Value
        {
            get => _bindingProperty != null ? _bindingProperty.Value : default;
            set
            {
                if (_bindingProperty != null)
                    _bindingProperty.Value = value;
            }
        }


        protected sealed override void Activate(BindingPath path)
        {
            _bindingProperty = this.FindBindingProperty(path, _value);
            if (_bindingProperty == null)
            {
                var contextName = this.FindContextName();
                Debug.LogWarning(
                    $"Property {path.PropertyName} was not founds in {contextName}! Insure that context was added " +
                    $"and has {path.PropertyName} readonly field.");
                enabled = false;
                return;
            }

            _bindingProperty.OnUpdateValue += UpdateValueHandler;
            UpdateValueHandler(_bindingProperty.Value);
        }

        protected sealed override void Deactivate()
        {
            if (_bindingProperty != null)
                _bindingProperty.OnUpdateValue -= UpdateValueHandler;
        }

        protected abstract void UpdateValueHandler(TValue value);
    }
}