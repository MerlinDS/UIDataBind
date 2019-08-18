using UIDataBindCore;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public abstract class AbstractBinder : MonoBehaviour, IBinder
    {
        public abstract IDataContext Context { get; }

        #region Unity Events

        private void OnEnable() => Bind();

        private void OnDisable() => Unbind();

        #endregion

        #region Bindings

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

        #endregion


    }
}