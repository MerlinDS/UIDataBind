using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Binders.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
#pragma warning disable 649
        [SerializeField, HideInInspector]
        private string _type;
        [SerializeField]
        private string _path;
        [SerializeField]
        private bool _isAbsolute;
#pragma warning restore 649

        private BindingPath _fullPath;
        private IEntityProvider _entity;

        public BindingPath Path
        {
            get
            {
                if (_fullPath == BindingPath.Empty)
                {
                    _fullPath = !_isAbsolute
                        ? BindingPath.BuildFrom((this.GetParentView()?.Path ?? BindingPath.Empty), _path)
                        : BindingPath.BuildFrom(_path);
                }
                return _fullPath;
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
            _fullPath = BindingPath.Empty;
            Unbind();
        }

        #endregion

        #region Abstractions

        protected abstract void Bind();
        protected abstract void Unbind();

        #endregion

        protected void SetDirty() =>
            _entity.SetDirty();

        protected void BroadcastEvent(ControlEvent type) =>
            _entity.BroadcastEvent(type);

        public override string ToString() =>
            $"{name} ({GetType().Name}";
    }
}