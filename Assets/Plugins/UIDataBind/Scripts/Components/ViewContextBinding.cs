using System;
using Plugins.UIDataBind.Base;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    [DisallowMultipleComponent]
    public class ViewContextBinding : BaseBinding
    {
        public virtual IViewContext Context { get; set; }

        protected sealed override void Activate(BindingPath path)
        {
            if (this is IViewContext)
                Context = this as IViewContext;

            if (Context == null)
                throw new InvalidOperationException($"{this} has no {typeof(IViewContext)} for binding!");

            //TODO: Register new context in kernel
        }

        protected sealed override void Deactivate()
        {
            //TODO: UnRegister existing context in kernel
        }
    }
}