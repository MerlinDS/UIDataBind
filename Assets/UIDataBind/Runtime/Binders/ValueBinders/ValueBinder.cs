using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Entitas.Wrappers;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    public abstract class ValueBinder<TValue> : BaseBinder, IValueBinder
    {
        [SerializeField]
        private TValue _value;

        public override void Bind() =>
            Engine.AddPropertyComponent(_value);

        public override void Unbind()
        {
        }

        public void Refresh() =>
            _value = Engine.GetPropertyValue<TValue>();
    }
}