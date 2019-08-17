using UIDataBindCore;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public abstract class PropertyBinder<TValue> : BaseBinder
    {

        #region Serialized fields

#pragma warning disable 0649
        [SerializeField]
        private TValue _value;
#pragma warning restore 0649

        #endregion
        public override void Bind()
        {
            Debug.Log(Context);
        }

        public override void Unbind()
        {
            Debug.Log(Context);
        }
    }
}