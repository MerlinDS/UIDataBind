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
    /// <summary>
    /// Base editor script for all of the <see cref="BaseBinding"/> component inspectors
    /// </summary>
    public abstract class BaseBindingInspector: UnityEditor.Editor
    {
        private static readonly BindingPath PathTemplate = new BindingPath();
        private readonly List<BaseBindingAttribute> _bindings = new List<BaseBindingAttribute>();

        private IViewContext _context;

        private SerializedProperty _type;
        private SerializedProperty _name;
        private SerializedProperty _path;

        private bool _showContext;

        protected BaseBinding Binding { get; private set; }
        protected SerializedProperty[] ExcludingProperties { get; set; }
        protected BindingType BindingType => (BindingType)_type.intValue;

        protected virtual void OnEnable()
        {
            _showContext = false;
            Binding = serializedObject.targetObject as BaseBinding;
            if (Binding == null)
                return;

            _path = serializedObject.FindProperty("_path");
            _type = _path.FindPropertyRelative(nameof(PathTemplate.Type));
            _name = _path.FindPropertyRelative(nameof(PathTemplate.Name));

            ExcludingProperties = new[] {_path};

            _bindings.Clear();

            _context = Binding.GetComponentInParent<IViewContext>();
            if (_context == null)
                return;

            _bindings.AddRange(GetAttributes(_context));
        }

        #region Drawing

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

            var typeContent = new GUIContent("Binding Type", "A type of binding for the current component");
            _type.intValue = EditorGUILayout.Popup(typeContent, _type.intValue, GetBindingTypeContents());

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

            if(serializedObject.DrawPropertiesExcluding(ExcludingProperties))
                ReactivateBinding();
        }

        /// <summary>
        /// The Internal GUI drawing method
        /// </summary>
        protected virtual void OnInternalGUI()
        {

        }

        private void DrawPathGUI()
        {

            EditorGUI.BeginChangeCheck();
            var index = _bindings.FindIndex(a => a.Name == _name.stringValue);
            EditorGUILayout.BeginHorizontal();

            var names = _bindings.Select(a=>new GUIContent(a.BindingName, a.Help)).ToArray();
            index = EditorGUILayout.Popup(new GUIContent(_name.name, "Name of a binding") , index, names);
            _showContext = EditorGUILayout.ToggleLeft(new GUIContent(string.Empty, "Show current context"),
                                                      _showContext, GUILayout.MaxWidth(30));

            EditorGUILayout.EndHorizontal();
            if (_showContext)
            {
                EditorGUILayout.ObjectField(new GUIContent("Context", "The component will be binned to this context"),
                                            (Component)_context, typeof(Component), false);
            }

            if (!EditorGUI.EndChangeCheck())
                return;

            _name.stringValue = index >= 0 ? _bindings[index].Name : string.Empty;
            serializedObject.ApplyModifiedProperties();

            ReactivateBinding();
        }

        #endregion

        /// <summary>
        /// This method will reactivate component in play mode
        /// </summary>
        protected void ReactivateBinding()
        {
            if (Application.isPlaying)
                Binding.Reactivate();
        }

        protected abstract IEnumerable<BaseBindingAttribute> GetAttributes(IViewContext context);

        public static GUIContent[] GetBindingTypeContents() =>
            Enum.GetValues(typeof(BindingType)).Cast<BindingType>().Select(e=>e.ToGUIContent()).ToArray();
    }
}