using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    public abstract class ValueToComponentBinder<TComponent, TValue> : ValueBinder<TValue>
        where TComponent : Component
    {
        private TComponent _component;

        protected TComponent Component
        {
            get
            {
                if (_component == null)
                    _component = GetComponent<TComponent>();
                return _component;
            }
        }
    }
}