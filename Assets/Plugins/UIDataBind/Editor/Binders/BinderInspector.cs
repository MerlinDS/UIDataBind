using Plugins.UIDataBind.Binders;
using Plugins.UIDataBind.Editor.Extensions;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Binders
{
    [CustomEditor(typeof(BaseBinder), true)]
    public class BinderInspector : UnityEditor.Editor
    {
        private SerializedProperty _bindingType;
        private SerializedProperty _path;

        private void OnEnable()
        {
            _bindingType = serializedObject.FindProperty(nameof(_bindingType));
            _path = serializedObject.FindProperty(nameof(_path));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_bindingType);
            if(_bindingType.enumValueIndex == (int)BindingType.Context)
                EditorGUILayout.PropertyField(_path);

            if(EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            serializedObject.DrawDefaultInspector(_bindingType, _path);
        }

    }
}
