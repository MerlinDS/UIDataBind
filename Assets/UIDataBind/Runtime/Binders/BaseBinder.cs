using UIDataBind.Base;
using UIDataBind.Base.Extensions;
using UIDataBind.Binders.Extensions;
using UnityEngine;

namespace UIDataBind.Binders
{
    public abstract class BaseBinder : MonoBehaviour, IBinder
    {
        private IEntityProvider _entity;

        private BindingPath _fullPath;

        // ReSharper disable once ConvertToAutoProperty
        public BindingType BindingType => _bindingType;

        public BindingPath Path
        {
            get
            {
                if (BindingType == BindingType.Self && !(this is IView))
                    return BindingPath.Empty;

                if (_fullPath == BindingPath.Empty)
                    _fullPath = BindingType == BindingType.Context ? GetContextPath() : GetAbsolutePath();

                return _fullPath;
            }
        }

        private BindingPath GetContextPath()
        {
            return BindingPath.BuildFrom(this.GetParentView()?.Path ?? BindingPath.Empty, _path);
        }

        private BindingPath GetAbsolutePath()
        {
            return string.IsNullOrEmpty(_path) ? BindingPath.Empty : BindingPath.BuildFrom(_path.Split('.'));
        }

        protected void SetDirty()
        {
            _entity?.SetDirty();
        }

        protected void BroadcastEvent(ControlEvent type)
        {
            _entity?.BroadcastEvent(type);
        }

        public override string ToString()
        {
            return $"{name} ({GetType().Name}";
        }
#pragma warning disable 649
        [SerializeField]
        [HideInInspector]
        private BindingType _bindingType = BindingType.Context;

        [SerializeField]
        [HideInInspector]
        // ReSharper disable once NotAccessedField.Local
        private string _type;

        [SerializeField]
        [HideInInspector]
        private string _path;
#pragma warning restore 649

        #region Unity Events

        private void OnEnable()
        {
            if (BindingType != BindingType.Self)
                _entity = this.GetEngine().CreateBinderEntity(this);
            Bind();
        }

        private void OnDisable()
        {
            Unbind();
            _entity?.Destroy();
            _fullPath = BindingPath.Empty;
        }

        #endregion

        #region Abstractions

        protected abstract void Bind();
        protected abstract void Unbind();

        #endregion
    }
}