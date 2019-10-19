using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Utils.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {

#pragma warning disable 649
        [SerializeField]
        private string _path;
#pragma warning restore 649

        private IEntityProvider _entity;

        // ReSharper disable once SuspiciousTypeConversion.Global
        public BindingPath Path => !(this is IView)
                ? GetComponentInParent<IView>()?.BuildPath(_path) ?? default
                : (BindingPath) _path;

        #region Unity Events

        private void OnEnable()
        {
            _entity = this.GetEngine().CreateBinderEntity(this);
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

        protected void SetDirty() => _entity.SetDirty();
        public override string ToString() => $"{name} ({GetType().Name}";
    }
}