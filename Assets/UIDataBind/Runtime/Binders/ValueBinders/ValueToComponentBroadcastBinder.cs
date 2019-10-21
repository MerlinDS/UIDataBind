using UIDataBind.Base;
using UnityEngine;

namespace UIDataBind.Binders.ValueBinders
{
    public abstract class ValueToComponentBroadcastBinder<TComponent, TValue> :
        ValueToComponentBinder<TComponent, TValue> where TComponent : Component
    {
#pragma warning disable 649
        [SerializeField]
        private bool _broadcastChangeEvent;
#pragma warning restore 649
        protected void ComponentHandler(TValue value)
        {
            Value = value;
            if(_broadcastChangeEvent)
                BroadcastEvent(ControlEvent.Changed);
            SetDirty();
        }
    }
}