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

        [SerializeField]
        private string _path;
#pragma warning restore 0649

        #endregion

        #region Properties

        protected string Path => _path;

        protected BindingType BindingType => _bindingType;

        #endregion

        #region Context

        private IDataContext _context;
        public IDataContext Context
        {
            get
            {
                if (_context == null)
                    _context = GetComponent<IDataContext>();
                return _context ?? (_context = GetComponentInParent<IDataContext>());
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