using UIDataBindCore;
using UnityEngine;

namespace Plugins.UIDataBind.Binders
{
    public class PropertyBinder<TValue> : BaseBinder
    {

        #region Serialized fields

#pragma warning disable 0649
        [SerializeField]
        private TValue _value;
#pragma warning restore 0649

        #endregion
        public override void Bind(IDataContext context)
        {
            throw new System.NotImplementedException();
        }

        public override void Unbind()
        {
            throw new System.NotImplementedException();
        }
    }
}