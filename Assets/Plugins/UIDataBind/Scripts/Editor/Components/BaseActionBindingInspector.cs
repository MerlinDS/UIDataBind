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
    [CustomEditor(typeof(BaseActionBinding), true)]
    public class BaseActionBindingInspector: UnityEditor.Editor
    {
        private static readonly BindingPath PathTemplate = new BindingPath();
        private readonly List<BindingMethodAttribute> _availableBindings = new List<BindingMethodAttribute>();

        private IViewContext _context;
        private BaseBinding _binding;

        private SerializedProperty _type;
        private SerializedProperty _name;
        private SerializedProperty _path;

        private SerializedProperty[] _excludingProperties;

        private BindingType BindingType => (BindingType)_type.intValue;

        private void OnEnable()
        {
            _binding = serializedObject.targetObject as BaseBinding;
            if (_binding == null)
                return;

            _path = serializedObject.FindProperty("_path");
            _type = _path.FindPropertyRelative(nameof(PathTemplate.Type));
            _name = _path.FindPropertyRelative(nameof(PathTemplate.Name));

            _excludingProperties = new[] {_path};
            _context = _binding.GetComponentInParent<IViewContext>();
            if (_context == null)
                return;

            _availableBindings.AddRange(_context.GetType().GetBindingMethodAttributes());
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


            serializedObject.DrawPropertiesExcluding(_excludingProperties);
        }

        private void DrawPropertyFieldGUI()
        {
            EditorGUI.BeginChangeCheck();
            var index = _availableBindings.FindIndex(a => a.Name == _name.stringValue);
            index = EditorGUILayout.Popup(_name.name, index,
                                          _availableBindings.Select(a=>a.BindingName).ToArray());
            if (!EditorGUI.EndChangeCheck())
                return;

            _name.stringValue = index >= 0 ? _availableBindings[index].Name : string.Empty;
            serializedObject.ApplyModifiedProperties();
        }

    }
}