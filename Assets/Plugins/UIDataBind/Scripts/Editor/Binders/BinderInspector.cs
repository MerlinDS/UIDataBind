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
        private IDataContext _context;

        public IDataContext Context => _context;

        protected virtual void OnEnable()
        {
            _path = serializedObject.FindProperty(nameof(_path));
            _bindingType = serializedObject.FindProperty(nameof(_bindingType));
            FindBoundContext();
        }

        private void FindBoundContext() =>
            _context = ((target as Component)?.GetComponent<IDataContextBinder>()
                        ?? (target as Component)?.GetComponentInParent<IDataContextBinder>())?.Context;

        private void OnDisable()
        {
            _context = null;
            _bindingType.Dispose();
            _path.Dispose();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(_bindingType);
            if (_bindingType.enumValueIndex == (int) BindingType.Context)
            {
                if (_context == null)
                {
                    EditorGUILayout.HelpBox("Can't find data context.\n" +
                                            "Add DataContextBinder to this game object, or to a parent game object.",
                                            MessageType.Error);
                    return;
                }

                OnBinding(_path);
            }

            if(EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            serializedObject.DrawDefaultInspector(_bindingType, _path);
        }

        protected abstract void OnBinding(SerializedProperty path);
    }
}
