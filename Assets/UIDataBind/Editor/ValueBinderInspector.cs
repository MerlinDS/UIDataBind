using UIDataBind.Binders.Attributes;
using UIDataBind.Binders.ValueBinders;
using UIDataBind.Editor.Utils;
using UnityEditor;

namespace UIDataBind.Editor
{
    [CustomEditor(typeof(ValueBinder<>), true)]
    public class ValueBinderInspector : BaseBinderInspector
    {
        private SerializedProperty _value;
        private ShowBinderValueAttribute _showValue;

        protected override void OnEnable()
        {
            base.OnEnable();
            _value = serializedObject.FindProperty("_value");
            _showValue = serializedObject.GetShowBinderValueAttribute();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            if(_showValue == null || !_showValue.Contains(BindingType))
                return;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_value);
            if (!EditorGUI.EndChangeCheck())
                return;

            serializedObject.ApplyModifiedProperties();
            /*if(Application.isPlaying)
                _resetMethod.Invoke();*/
        }
    }
}