using UIDataBind.Base;
using UIDataBind.Utils.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        [SerializeField]
        private string _path;

        private UiBindEntity _entity;

        // ReSharper disable once SuspiciousTypeConversion.Global
        public BindingPath Path => !(this is IView)
                ? GetComponentInParent<IView>()?.BuildPath(_path) ?? default
                : (BindingPath) _path;

        #region Unity Events

        private void OnEnable()
        {
            _entity = Contexts.sharedInstance.uiBind.CreateEntity();
            _entity.AddBinder(this);
            _entity.AddBindingPath(Path);
            Bind();
        }

        private void OnDisable()
        {
            _entity.Destroy();
            Unbind();
        }

        private void OnDestroy() => Dispose();

        #endregion

        #region Abstractions

        protected abstract void Bind();
        protected abstract void Unbind();
        protected abstract void Dispose();

        #endregion

        public override string ToString() => $"{name} ({GetType().Name})";
    }
}