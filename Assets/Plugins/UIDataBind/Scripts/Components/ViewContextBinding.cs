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
            //TODO: Register new context in kernel
            throw new System.NotImplementedException();
        }

        protected sealed override void Deactivate()
        {
            //TODO: UnRegister existing context in kernel
            throw new System.NotImplementedException();
        }
    }
}