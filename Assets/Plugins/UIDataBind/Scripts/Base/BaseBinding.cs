using Plugins.UIDataBind.Attributes;
using UnityEngine;

namespace Plugins.UIDataBind.Base
{
    public abstract class BaseBinding : BaseBindingBehaviour
    {
        [SerializeField]
        private BindingType _type = BindingType.View;
        [SerializeField, DataBindingPath]
        private string _path;
    }
}