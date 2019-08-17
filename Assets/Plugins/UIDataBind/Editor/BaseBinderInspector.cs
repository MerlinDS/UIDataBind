using UnityEditor;

namespace Plugins.UIDataBind.Editor
{
//    [CustomEditor(typeof(BaseBinder), true)]
    public class PropertyBinderInspector : UnityEditor.Editor
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
            serializedObject.Update();

            EditorGUILayout.PropertyField(_bindingType);
            EditorGUILayout.PropertyField(_path);
        }

    }
}
