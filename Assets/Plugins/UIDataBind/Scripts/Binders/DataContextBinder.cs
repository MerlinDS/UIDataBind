using UIDataBindCore;
using UIDataBindCore.Extensions;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public interface IDataContextBinder
    {
        IDataContext Context { get; }
    }
    public class DataContextBinder : MonoBehaviour, IDataContextBinder, IBinder
    {
        public virtual IDataContext Context { get; }

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