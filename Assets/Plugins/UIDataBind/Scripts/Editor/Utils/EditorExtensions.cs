using System;
using System.Linq;
using Plugins.UIDataBind.Base;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Utils
{
    public static class EditorExtensions
    {
        public static bool DrawPropertiesExcluding(this SerializedObject serializedObject,
            params SerializedProperty[] excludedProperties)
        {
            var excludedPath = excludedProperties.Select(p => p.propertyPath).ToArray();

            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();
            var iterator = serializedObject.GetIterator();
            for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if("m_Script" == iterator.propertyPath || excludedPath.Contains(iterator.propertyPath))
                    continue;

                EditorGUILayout.PropertyField(iterator, true);
            }

            serializedObject.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }

        public static GUIContent ToGUIContent(this BindingType type) =>
            new GUIContent(type.ToString(), type.GetHelpString());
    }
}