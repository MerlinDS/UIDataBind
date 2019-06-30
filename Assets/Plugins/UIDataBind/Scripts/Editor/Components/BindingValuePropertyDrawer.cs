using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Components
{
    [CustomPropertyDrawer(typeof(BindingValueAttribute))]
    public class BindingValuePropertyDrawer : PropertyDrawer
    {
        private static readonly BindingPath PathTemplate = new BindingPath();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if(!IsVisible(property))
                return;

            EditorGUI.PropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            IsVisible(property) ? base.GetPropertyHeight(property, label) : 0;

        private bool IsVisible(SerializedProperty property)
        {
            var bindingValueAttribute = (BindingValueAttribute) attribute;

            var type = property.serializedObject.FindProperty("_path")
                .FindPropertyRelative(nameof(PathTemplate.Type));

            var isVisible = true;
            if ((BindingType) type.intValue != BindingType.None)
                isVisible = bindingValueAttribute.VisibleWhenHasContext;
            return isVisible;
        }


    }
}