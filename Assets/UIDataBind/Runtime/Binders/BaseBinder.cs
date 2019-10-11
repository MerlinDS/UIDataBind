using UIDataBind.Base;
using UIDataBind.Utils.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        [SerializeField]
        private string _path;

        protected IEntityProvider Engine;


        public string Path => !(this is IView)
            ? GetComponentInParent<IView>()?.Path?.BuildPath(_path)
            : _path;


        #region Unity Events

        private void OnEnable()
        {
            Engine = this.GetEngineProvider(Path);
            Bind();
        }

        private void OnDisable()
        {
            Unbind();
            Engine.Destroy();
        }

        #endregion

        #region Bindings

        /// <inheritdoc/>
        public abstract void Bind();

        /// <inheritdoc/>
        public abstract void Unbind();

        #endregion

        public override string ToString() => name;
    }
}