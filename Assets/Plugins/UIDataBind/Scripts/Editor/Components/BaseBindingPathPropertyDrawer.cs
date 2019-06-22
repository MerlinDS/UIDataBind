using System;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Scripts.Editor.Components
{
    [CustomPropertyDrawer(typeof(DataBindingPathAttribute), true)]
    public class BaseBindingPathPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = property.serializedObject.FindProperty("_type");
            if(type.intValue == (int) BindingType.View)
                DrawViewBinding(position, property);
        }

        private void DrawViewBinding(Rect position, SerializedProperty property)
        {
            var availableProperties = GetViewProperties();
            var propertyIndex = Array.IndexOf(availableProperties, property.stringValue);
            EditorGUI.BeginChangeCheck();
            propertyIndex = EditorGUI.Popup(position, "Property", propertyIndex, availableProperties);
            if (EditorGUI.EndChangeCheck() && propertyIndex >= 0)
                property.stringValue = availableProperties[propertyIndex];
        }

        private string[] GetViewProperties()
        {
            return new[]
            {
                "Visible",
                "SampleImage"
            };
        }
    }
}