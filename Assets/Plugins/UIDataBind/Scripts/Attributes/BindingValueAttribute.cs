using UnityEngine;

namespace Plugins.UIDataBind.Attributes
{
    public class BindingValueAttribute : PropertyAttribute
    {
        public bool VisibleWhenHasContext { get; }
        public BindingValueAttribute(bool visibleWhenHasContext = false)
        {
            VisibleWhenHasContext = visibleWhenHasContext;
        }

    }
}