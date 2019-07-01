using Plugins.UIDataBind.Base;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    /// <summary>
    /// The base binding <see cref="MonoBehaviour"/>, used to bind components to a <see cref="IViewContext"/>.
    /// <seealso cref="BindingType"/>
    /// </summary>
    public abstract class BaseBinding : MonoBehaviour
    {
#pragma warning disable 0649
        [HideInInspector]
        [SerializeField]
        private BindingPath _path;

        protected BindingPath Path => _path;
#pragma warning restore 0649

        #region Unity Events

        private void OnEnable() => Activate(Path);

        private void OnDisable() => Deactivate();

        #endregion

        #region Abstract API

        protected abstract void Activate(BindingPath path);
        protected abstract void Deactivate();

        #endregion

        public void Reactivate()
        {
            Deactivate();
            Activate(Path);
        }
    }
}