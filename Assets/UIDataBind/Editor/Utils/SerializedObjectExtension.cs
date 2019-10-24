using System.Linq;
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
    }
}