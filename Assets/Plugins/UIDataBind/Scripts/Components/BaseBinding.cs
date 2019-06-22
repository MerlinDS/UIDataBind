using Plugins.UIDataBind.Base;
using UnityEngine;

namespace Plugins.UIDataBind.Components
{
    public abstract class BaseBinding : MonoBehaviour
    {
        [SerializeField]
        private BindingPath _path;

        #region Unity Events

        private void OnEnable() => Activate(_path);

        private void OnDisable() => Deactivate();

        #endregion

        #region Abstract API

        protected abstract void Activate(BindingPath path);
        protected abstract void Deactivate();

        #endregion
    }
}