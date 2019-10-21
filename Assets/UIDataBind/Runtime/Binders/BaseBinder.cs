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

        public BindingPath Path => !(this is IView)
            ? ParentPath.BuildPath(_path)
            : (BindingPath) _path;

        public BindingPath ParentPath => GetComponentInParent<IView>()?.Path ?? default;

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

        #endregion

        #region Abstractions

        protected abstract void Bind();
        protected abstract void Unbind();

        #endregion

        protected       void   SetDirty() => _entity.SetDirty();
        protected void BroadcastEvent(ControlEvent type) => _entity.BroadcastEvent(type);
        public override string ToString() => $"{name} ({GetType().Name}";

    }
}