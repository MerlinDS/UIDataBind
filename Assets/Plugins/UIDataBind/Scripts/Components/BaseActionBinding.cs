using System;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Extensions;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public abstract class BaseActionBinding : BaseBinding
    {
        protected Action ExternalAction { get; private set; }
        protected sealed override void Activate(BindingPath path)
        {
            ExternalAction = this.FindBindingMethod(path);
            if (ExternalAction == null)
            {
                var contextName = this.FindContextName();
                Debug.LogWarning(
                    $"Action {path.Name} was not founds in {contextName}! Insure that context was added " +
                    $"and has {path.Name} method.");
                enabled = false;
            }
            Subscribe();
        }


        protected sealed override void Deactivate()
        {
            Unsubscribe();
            ExternalAction = null;
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
    }
}