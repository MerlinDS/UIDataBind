using System;
using UIDataBindCore;
using UIDataBindCore.Extensions;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    /// <summary>
    /// The base binding for actions, used to bind action to a <see cref="IDataContext"/> <see cref="Action">methods</see>.
    /// </summary>
    /// <seealso cref="BaseBinder"/>
    /// <seealso cref="BindingType"/>
    public abstract class ActionBinder : BaseBinder
    {
        private Action _externalAction;

        public override void Bind()
        {
            Unbind();
            if(BindingType != BindingType.Context)
                return;

            _externalAction = Context.FindMethod(Path);
            if (_externalAction == null)
            {
                Debug.LogWarning(
                    $"Action {Path} was not founds in {Context}! Insure that context was added " +
                    $"and has {Path} method.");
                enabled = false;
                return;
            }
            Subscribe();
        }

        public override void Unbind()
        {
            Unsubscribe();
            _externalAction = null;
        }

        protected void InvokeAction() => _externalAction?.Invoke();

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();
    }
}