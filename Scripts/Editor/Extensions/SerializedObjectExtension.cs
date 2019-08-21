using System.Linq;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Extensions
{
    public static class SerializedObjectExtension
    {
        public static bool DrawDefaultInspector(this SerializedObject serializedObject,
            params SerializedProperty[] excludedProperties)
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();
            var iterator = serializedObject.GetIterator();
            for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if("m_Script" != iterator.propertyPath && excludedProperties.All(x => x.propertyPath != iterator.propertyPath))
                    EditorGUILayout.PropertyField(iterator, true);
            }
            serializedObject.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }
    }
}