using System;
using System.Linq;
using UIDataBind.Binders.Attributes;
using UnityEditor;

namespace UIDataBind.Editor.Utils
{
    public static class SerializedObjectExtension
    {
        public static bool DoDrawDefaultInspector(this SerializedObject obj, params SerializedProperty[] property) =>
            obj.DoDrawDefaultInspector(property.Select(x => x.propertyPath).ToArray());

        private static bool DoDrawDefaultInspector(this SerializedObject obj, string[] property)
        {

            EditorGUI.BeginChangeCheck();
            obj.UpdateIfRequiredOrScript();
            var iterator = obj.GetIterator();
            for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if(!property.Contains(iterator.propertyPath) && "m_Script" != iterator.propertyPath)
                    EditorGUILayout.PropertyField(iterator, true);
            }
            obj.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }

        public static ShowBinderValueAttribute GetShowBinderValueAttribute(this SerializedObject serializedObject)
        {
            var type = serializedObject.targetObject.GetType();
            if(Attribute.IsDefined(type, typeof(ShowBinderValueAttribute)))
                return (ShowBinderValueAttribute) Attribute.GetCustomAttribute(type, typeof(ShowBinderValueAttribute), true);
            return null;
        }
    }
}