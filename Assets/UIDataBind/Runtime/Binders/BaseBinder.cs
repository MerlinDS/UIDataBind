using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UIDataBind.Utils.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        [SerializeField]
        private string _path;

        public IEntityProvider Engine { get; set; }


        public string Path => !(this is IView)
            ? GetComponentInParent<IView>()?.Path?.BuildPath(_path)
            : _path;


        #region Unity Events

        private void OnEnable()
        {
            Engine = this.GetEngineProvider();
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