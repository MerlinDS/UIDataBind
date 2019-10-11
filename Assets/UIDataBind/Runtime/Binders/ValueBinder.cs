using UnityEngine;

namespace UIDataBind.Binders
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

    public interface IValueBinder
    {
        void Refresh();
    }
}