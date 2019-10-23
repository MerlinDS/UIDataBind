using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Binders.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
#pragma warning disable 649
        [SerializeField]
        private string _path;
#pragma warning restore 649

        private BindingPath _parentPath;
        private IEntityProvider _entity;

        public BindingPath Path
        {
            get
            {
                if (_parentPath == BindingPath.Empty)
                    _parentPath = this.GetParentView()?.Path ?? BindingPath.Empty;
                return BindingPath.BuildFrom(_parentPath, _path);
            }
        }

        #region Unity Events

        private void OnEnable()
        {
            _entity = this.GetEngine().CreateBinderEntity(this);
            Bind();
        }

        private void OnDisable()
        {
            _entity.Destroy();
            _parentPath = BindingPath.Empty;
            Unbind();
        }

        #endregion

        #region Abstractions

        protected abstract void Bind();
        protected abstract void Unbind();

        #endregion

        protected       void   SetDirty()                        => _entity.SetDirty();
        protected       void   BroadcastEvent(ControlEvent type) => _entity.BroadcastEvent(type);
        public override string ToString()                        => $"{name} ({GetType().Name}";
    }
}