using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Binders.Extensions;
using UIDataBind.Runtime.Base.Extensions;
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


        private OldBindingPath _fullPath;
        private OldBindingPath _parentPath;
        public OldBindingPath Path
        {
            get
            {
                if (_fullPath.IsEmpty)
                    _fullPath = ParentPath.IsEmpty ? (OldBindingPath) _path : ParentPath.BuildPath(_path);
                return _fullPath;
            }
        }

        public OldBindingPath ParentPath
        {
            get
            {
                if (_parentPath.IsEmpty)
                    _parentPath = this.GetParentView()?.Path??string.Empty;
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
            _parentPath = _fullPath = string.Empty;
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