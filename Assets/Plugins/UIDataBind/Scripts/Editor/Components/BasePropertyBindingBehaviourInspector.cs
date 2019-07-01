using System;
using System.Collections.Generic;
using System.Linq;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Components
{
    [CustomEditor(typeof(BasePropertyBindingBehaviour<>), true)]
    public sealed class BasePropertyBindingBehaviourInspector : UnityEditor.Editor
    {
        private static readonly BindingPath PathTemplate = new BindingPath();
        private readonly List<BindingPropertyAttribute> _availableFields = new List<BindingPropertyAttribute>();

        private IViewContext _context;
        private BaseBinding _binding;

        private SerializedProperty _type;
        private SerializedProperty _propertyName;
        private SerializedProperty _path;
        private SerializedProperty _defaultValue;
        private SerializedProperty[] _excludingProperties;

        private bool _isValueDrawable = true;
        private bool _isAlwaysShowValue;

        private BindingType BindingType => (BindingType)_type.intValue;


        private void OnEnable()
        {
            _binding = serializedObject.targetObject as BaseBinding;
            if (_binding == null)
                return;

            _isValueDrawable = !Attribute.IsDefined(_binding.GetType(), typeof(HideBindingValueAttribute));
            if(_isValueDrawable)
                _isAlwaysShowValue = Attribute.IsDefined(_binding.GetType(), typeof(ShowBindingValueAttribute));

            _path = serializedObject.FindProperty("_path");
            _defaultValue = serializedObject.FindProperty("_value");
            _type = _path.FindPropertyRelative(nameof(PathTemplate.Type));
            _propertyName = _path.FindPropertyRelative(nameof(PathTemplate.PropertyName));

            _excludingProperties = new[] {_path, _defaultValue};

            _context = _binding.GetComponentInParent<IViewContext>();
            _availableFields.Clear();

            if (_context == null)
                return;

            var expectedType = ((IPropertyBindingBehaviour)_binding).GetValueType;
            _availableFields.AddRange(_context.GetType().GetBindingPropertyAttributes(expectedType));
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
            if (_context == null && BindingType != BindingType.None)
            {
                EditorGUILayout.HelpBox("Can't finds context for this binding.", MessageType.Warning);
                _type.intValue = (int) BindingType.None;
            }

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            if (BindingType != BindingType.None)
                DrawPropertyFieldGUI();

            if(_isAlwaysShowValue || BindingType == BindingType.None)
            {
                EditorGUI.BeginChangeCheck();
                if(_isValueDrawable)
                    EditorGUILayout.PropertyField(_defaultValue);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
            }


            serializedObject.DrawPropertiesExcluding(_excludingProperties);
        }

        private void DrawPropertyFieldGUI()
        {
            EditorGUI.BeginChangeCheck();
            var index = _availableFields.FindIndex(a => a.Name == _propertyName.stringValue);
            index = EditorGUILayout.Popup(_propertyName.name, index,
                                          _availableFields.Select(a=>a.BindingName).ToArray());
            if (!EditorGUI.EndChangeCheck())
                return;

            _propertyName.stringValue = index >= 0 ? _availableFields[index].Name : string.Empty;
            serializedObject.ApplyModifiedProperties();
        }
    }
}