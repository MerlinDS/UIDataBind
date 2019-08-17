using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Binders;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor
{
    [CustomPropertyDrawer(typeof(BindingPathAttribute))]
    public class BindingPathPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsContextBindingType(property))
                return;

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property);
            EditorGUI.EndProperty();
        }

        private static bool IsContextBindingType(SerializedProperty property) =>
            property.serializedObject.FindProperty("_bindingType").enumValueIndex != (int) BindingType.Context;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            IsContextBindingType(property) ? 0F : base.GetPropertyHeight(property, label);
    }
}