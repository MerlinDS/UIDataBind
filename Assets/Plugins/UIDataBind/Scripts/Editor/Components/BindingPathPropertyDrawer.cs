using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Properties;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Components
{
//    [CustomPropertyDrawer(typeof(BindingPath))]
    public class BindingPathPropertyDrawer : PropertyDrawer
    {
        private static readonly BindingPath Template = new BindingPath();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject;
            var binding = targetObject as BaseBinding;
            if (binding == null)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            var prefixLabel = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var typeProperty = GetTypeFieldFrom(property);
            var rect = new Rect(position.x, position.y + prefixLabel.y, position.width,
                                base.GetPropertyHeight(typeProperty, label));

            var context = GetContext(binding);
            if (context == null && typeProperty.intValue > 0)
            {
                Debug.LogError($"You should add {nameof(IViewContext)}!");
                typeProperty.intValue = 0;
            }

            EditorGUI.PropertyField(rect, typeProperty);

            if (IsPropertyNameSelectable(property) && context != null)
                DrawPropertyNameSelection(rect, property, context);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private void DrawPropertyNameSelection(Rect position, SerializedProperty property, IViewContext context)
        {
            var properties = GetBindingPropertiesFrom(context);
            var availableProperties = properties.Select(p => p.Name).ToArray();


            var propertyNameProperty = GetPropertyNameFieldFrom(property);
            position.y += position.height + 4;
            EditorGUI.Popup(position, propertyNameProperty.name, -1, availableProperties);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!(property.serializedObject.targetObject is BaseBinding))
                return base.GetPropertyHeight(property, label);

            var countOfProperties = IsPropertyNameSelectable(property) ? 3 : 2;
            return base.GetPropertyHeight(property, label) * countOfProperties + 4;
        }

        private static IViewContext GetContext(Component binding) =>
            binding.GetComponentInParent<IViewContext>();

        private bool IsPropertyNameSelectable(SerializedProperty property) =>
            GetTypeFieldFrom(property).intValue > 0;

        private SerializedProperty GetTypeFieldFrom(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(Template.Type));

        private SerializedProperty GetPropertyNameFieldFrom(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(Template.PropertyName));

        private static IEnumerable<FieldInfo> GetBindingPropertiesFrom(IViewContext context)
        {
            return context.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => typeof(IBindingProperty).IsAssignableFrom(f.FieldType));
        }
    }
}