using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    public abstract class ValueBinder<TValue> : BaseBinder, IValueBinder
    {
        [SerializeField]
        private TValue _value;

        protected TValue Value
        {
            get => _value;
            set => _value = value;
        }

        public override void Bind() =>
            Engine.AddPropertyComponent(_value);

        public override void Unbind()
        {
        }

        public void Refresh()
        {
            _value = Engine.GetPropertyValue<TValue>();
            UpdateValueHandler(_value);
        }

        protected abstract void UpdateValueHandler(TValue value);
    }
}