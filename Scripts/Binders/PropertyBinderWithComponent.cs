using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public abstract class PropertyBinderWithComponent<TComponent, TValue> : PropertyBinder<TValue>
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