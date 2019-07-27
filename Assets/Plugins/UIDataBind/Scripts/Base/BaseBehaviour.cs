using System;
using UnityEngine;

namespace Plugins.UIDataBind.Base
{
    /// <summary>
    /// Base <see cref="MonoBehaviour"/> class for all components in IUDataBind
    /// </summary>
    public abstract class BaseBehaviour : MonoBehaviour, IDisposable
    {
        #region Unity Events

        private void OnEnable() => Activate();

        private void OnDisable() => Deactivate();

        #endregion

        #region Abstract API

        /// <summary>
        /// Activation actions of a component
        /// </summary>
        protected abstract void Activate();
        /// <summary>
        /// Deactivation actions of a component
        /// </summary>
        protected abstract void Deactivate();

        #endregion

        #region Public API

        public void Reactivate()
        {
            Deactivate();
            Activate();
        }

        public virtual void Dispose()
        {

        }

        #endregion
    }
}