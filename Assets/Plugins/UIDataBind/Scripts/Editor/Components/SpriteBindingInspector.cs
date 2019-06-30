using System.Collections.Generic;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Components;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Components
{
    [CustomEditor(typeof(SpriteBinding), true)]
    public class SpriteBindingInspector : BasePropertyBindingBehaviourInspector
    {
        protected override bool IsValueDrawable => false;
        private SerializedProperty[] _noneExcludingProperties;
        private SerializedProperty _sprite;

        protected override void OnEnable()
        {
            base.OnEnable();

            _noneExcludingProperties = new List<SerializedProperty>(base.GetExcludingProperties())
            {
                serializedObject.FindProperty("_sprite")
            }.ToArray();
        }

        protected override SerializedProperty[] GetExcludingProperties() =>
            BindingType == BindingType.None ? base.GetExcludingProperties() : _noneExcludingProperties;
    }
}