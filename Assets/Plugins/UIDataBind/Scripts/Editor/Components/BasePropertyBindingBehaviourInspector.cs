using System;
using System.Linq;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Components
{
    [CustomEditor(typeof(BasePropertyBindingBehaviour<>), true)]
    public class BasePropertyBindingBehaviourInspector : UnityEditor.Editor
    {
        private static readonly BindingPath PathTemplate = new BindingPath();

        private IViewContext _context;
        private BaseBinding _binding;

        private SerializedProperty _type;
        private SerializedProperty _propertyName;
        private string[] _availableFields;
        private SerializedProperty _path;

        private void OnEnable()
        {
            _binding = serializedObject.targetObject as BaseBinding;
            if (_binding == null)
                return;

            _path = serializedObject.FindProperty("_path");
            _type = _path.FindPropertyRelative(nameof(PathTemplate.Type));
            _propertyName = _path.FindPropertyRelative(nameof(PathTemplate.PropertyName));

            _context = _binding.GetComponentInParent<IViewContext>();
            _availableFields = CollectPropertyFields(_binding, _context);
        }

        private static string[] CollectPropertyFields(BaseBinding binding, IViewContext context)
        {
            if (context == null)
                return new string[0];

            var bindingType = binding.GetType().GetFirstGenericTypeFrom(typeof(IPropertyBindingBehaviour<>));
            return context.GetType().GetBindingPropertiesInfo(bindingType).Select(i => i.Name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            if (_binding == null)
            {
                base.OnInspectorGUI();
                return;
            }

            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_type, new GUIContent("Binding Type"));
            if (_context == null && _type.intValue != (int) BindingType.None)
            {
                EditorGUILayout.HelpBox("Can't finds context for this binding.", MessageType.Warning);
                _type.intValue = (int) BindingType.None;
            }

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            if (_type.intValue != (int) BindingType.None)
                DrawPropertyFieldGUI();

            serializedObject.DrawPropertiesExcluding(_path);
        }

        private void DrawPropertyFieldGUI()
        {
            EditorGUI.BeginChangeCheck();
            var index = Array.IndexOf(_availableFields, _propertyName.stringValue);
            index = EditorGUILayout.Popup(_propertyName.name, index,
                                          _availableFields.ConvertToHumanReadtable().ToArray());
            if (!EditorGUI.EndChangeCheck())
                return;

            _propertyName.stringValue = index >= 0 ? _availableFields[index] : string.Empty;
            serializedObject.ApplyModifiedProperties();
        }
    }
}