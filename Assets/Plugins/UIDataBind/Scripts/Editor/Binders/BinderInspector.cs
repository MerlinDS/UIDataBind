using System.Collections.Generic;
using System.Linq;
using Plugins.UIDataBind.Binders;
using Plugins.UIDataBind.Editor.Extensions;
using UIDataBindCore;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Binders
{
    public abstract class BinderInspector : UnityEditor.Editor
    {
        private SerializedProperty _bindingType;
        private SerializedProperty _path;

        protected IDataContext Context { get; private set; }
        protected BindingType BindingType { get; private set; }

        private readonly HashSet<SerializedProperty> _excludedProperties = new HashSet<SerializedProperty>();

        protected virtual void OnEnable()
        {
            _path = serializedObject.FindProperty(nameof(_path));
            _bindingType = serializedObject.FindProperty(nameof(_bindingType));

            AddExcludedProperties(_path, _bindingType);
            FindBoundContext();
        }

        private void FindBoundContext() =>
            Context = ((target as Component)?.GetComponent<IDataContextBinder>()
                       ?? (target as Component)?.GetComponentInParent<IDataContextBinder>())?.Context;

        private void OnDisable()
        {
            Context = null;
            _bindingType.Dispose();
            _path.Dispose();
        }

        public sealed override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUI.BeginChangeCheck();

            BindingType = (BindingType) _bindingType.enumValueIndex;
            EditorGUILayout.PropertyField(_bindingType);
            if (BindingType == BindingType.Context)
            {
                if (Context == null)
                {
                    EditorGUILayout.HelpBox("Can't find data context.\n" +
                                            "Add DataContextBinder to this game object, or to a parent game object.",
                                            MessageType.Error);
                    return;
                }

                OnBinding(_path);
            }

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            if (BindingType != (BindingType) _bindingType.enumValueIndex && Application.isPlaying)
                (serializedObject.targetObject as IBinder)?.Bind();

            BindingType = (BindingType) _bindingType.enumValueIndex;
            OnGUI();
            serializedObject.DrawDefaultInspector(_excludedProperties.ToArray());
        }

        protected void AddExcludedProperties(params SerializedProperty[] properties)
        {
            foreach (var property in properties)
                _excludedProperties.Add(property);
        }

        protected virtual void OnBinding(SerializedProperty path)
        {
        }

        protected virtual void OnGUI()
        {
        }
    }
}