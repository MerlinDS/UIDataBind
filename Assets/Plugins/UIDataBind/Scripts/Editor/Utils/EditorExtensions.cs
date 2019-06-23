using System.Linq;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Utils
{
    public static class EditorExtensions
    {
        public static void DrawPropertiesExcluding(this SerializedObject serializedObject,
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
            EditorGUI.EndChangeCheck();
        }
    }
}