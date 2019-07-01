using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public abstract class BasePropertyBindingWithComponent<TComponent, TValue> :
        BasePropertyBinding<TValue>
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