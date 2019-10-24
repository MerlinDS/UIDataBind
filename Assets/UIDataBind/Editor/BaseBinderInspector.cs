using System;
using System.Linq;
using UIDataBind.Base;
using UIDataBind.Binders;
using UIDataBind.Binders.Extensions;
using UIDataBind.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace UIDataBind.Editor
{
    [CustomEditor(typeof(BaseBinder), true)]
    public class BaseBinderInspector : UnityEditor.Editor
    {
        private SerializedProperty _type;
        private SerializedProperty _path;
        private SerializedProperty _isAbsolute;

        private bool _isView;
        private Type _valueType;

        private void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _path = serializedObject.FindProperty("_path");
            _isAbsolute = serializedObject.FindProperty("_isAbsolute");

            if (target is IValueBinder binder)
                _valueType = binder.ValueType;
            _isView = target is IView;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (_isView)
                _type.stringValue = BinderGUILayout.Popup(_type.stringValue, EditorModelUtils.GetModelNames());
            DrawPath(_isView);

            serializedObject.ApplyModifiedProperties();
            serializedObject.DoDrawDefaultInspector(_path, _type, _isAbsolute);
        }

        private void DrawPath(bool isView)
        {
            var isAbsolute = _isAbsolute.boolValue;
            var fieldInfos = EditorModelUtils.GetFields(FullModelName, _valueType);
            if (!isView && fieldInfos.Length == 0)
                isAbsolute = true;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel($"{_path.displayName} {(isAbsolute ? "(absolute)" : string.Empty)}");

            isAbsolute = EditorGUILayout.Toggle(isAbsolute, GUILayout.MaxWidth(20));
            _path.stringValue = !isAbsolute && !isView
                ? BinderGUILayout.Popup(_path.stringValue, fieldInfos.Select(x => x.Name))
                : EditorGUILayout.TextField(_path.stringValue);

            _isAbsolute.boolValue = isAbsolute;

            EditorGUILayout.EndHorizontal();
        }

        private string FullModelName
        {
            get
            {
                if (_isView)
                    return _type.stringValue;

                var parentView = ((BaseBinder) target).GetParentView() as Component;
                return parentView == null
                    ? string.Empty
                    : new SerializedObject(parentView).FindProperty("_type")?.stringValue;
            }
        }
    }
}