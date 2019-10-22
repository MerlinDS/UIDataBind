using UIDataBind.Base;
using UIDataBind.Binders.Extensions;
using UIDataBind.Runtime.Base.Extensions;
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


        private BindingPath _fullPath;
        private BindingPath _parentPath;

        public BindingPath Path
        {
            get
            {
                if (_fullPath == BindingPath.Empty)
                    _fullPath = BindingPath.BuildFrom(ParentPath, _path);
                return _fullPath;
            }
        }

        public BindingPath ParentPath
        {
            get
            {
                if (_parentPath == BindingPath.Empty)
                    _parentPath = this.GetParentView()?.Path ?? BindingPath.Empty;
                return _parentPath;
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
            _parentPath = _fullPath = BindingPath.Empty;
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