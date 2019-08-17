using Plugins.UIDataBind.Attributes;
using UIDataBindCore;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    /// <summary>
    /// The base binding <see cref="MonoBehaviour"/>, used to bind components to a <see cref="IDataContext"/>.
    /// </summary>
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        #region Serialized Fields

#pragma warning disable 0649

        [SerializeField]
        private BindingType _bindingType;

        [BindingPath, SerializeField]
        private string _path;
#pragma warning restore 0649

        #endregion

        #region Properties

        protected string Path => _path;

        protected BindingType BindingType => _bindingType;

        #endregion

        #region Context

        private IDataContextBinder _dataContextBinder;
        public IDataContext Context
        {
            get
            {
                if (_dataContextBinder == null)
                    _dataContextBinder = GetComponent<IDataContextBinder>();
                if (_dataContextBinder == null)
                    _dataContextBinder = GetComponentInParent<IDataContextBinder>();

                return _dataContextBinder?.Context;
            }
        }

        #endregion

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