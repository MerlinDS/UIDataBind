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
    public abstract class BaseBindingInspector: UnityEditor.Editor
    {
        private static readonly BindingPath PathTemplate = new BindingPath();
        private readonly List<BaseBindingAttribute> _availableBindings = new List<BaseBindingAttribute>();

        private IViewContext _context;

        private SerializedProperty _type;
        private SerializedProperty _name;
        private SerializedProperty _path;

        protected BaseBinding Binding { get; set; }
        protected SerializedProperty[] ExcludingProperties { get; set; }
        protected BindingType BindingType => (BindingType)_type.intValue;

        protected virtual void OnEnable()
        {
            Binding = serializedObject.targetObject as BaseBinding;
            if (Binding == null)
                return;

            _path = serializedObject.FindProperty("_path");
            _type = _path.FindPropertyRelative(nameof(PathTemplate.Type));
            _name = _path.FindPropertyRelative(nameof(PathTemplate.Name));

            ExcludingProperties = new[] {_path};

            _availableBindings.Clear();

            _context = Binding.GetComponentInParent<IViewContext>();
            if (_context == null)
                return;

            _availableBindings.AddRange(GetAttributes(_context));
        }

        public sealed override void OnInspectorGUI()
        {
            if (Binding == null)
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
                DrawPathGUI();

            OnInternalGUI();
            serializedObject.DrawPropertiesExcluding(ExcludingProperties);
        }

        protected virtual void OnInternalGUI()
        {

        }

        private void DrawPathGUI()
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

        protected abstract IEnumerable<BaseBindingAttribute> GetAttributes(IViewContext context);
    }
}