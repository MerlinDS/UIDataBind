using System.Collections.Generic;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Editor.Utils;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Components
{
    [CustomEditor(typeof(BaseActionBinding), true)]
    public class BaseActionBindingInspector: BaseBindingInspector
    {
        protected override IEnumerable<BaseBindingAttribute> GetAttributes(IViewContext context) =>
            context.GetType().GetBindingMethodAttributes();
    }
}