using UIDataBindCore;
using UIDataBindCore.Extensions;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public class DataContextBinder<TContext> : DataContextBinder
        where TContext : class, IDataContext, new()
    {
        public override IDataContext Context { get; } = new TContext();
    }

    public abstract class DataContextBinder : MonoBehaviour, IDataContextBinder
    {
        public abstract IDataContext Context { get; }

        #region Unity Events

        private void OnEnable() => Bind();

        private void OnDisable() => Unbind();

        #endregion


        #region Bindings

        /// <inheritdoc/>
        public void Bind() => Context.Register();

        /// <inheritdoc/>
        public void Unbind() => Context.Unregister();

        #endregion
    }
}