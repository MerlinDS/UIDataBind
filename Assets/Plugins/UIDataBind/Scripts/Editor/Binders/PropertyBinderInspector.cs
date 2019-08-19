using System;
using System.Linq;
using Plugins.UIDataBind.Binders;
using Plugins.UIDataBind.Editor.Extensions;
using UIDataBindCore;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Binders
{
    [CustomEditor(typeof(PropertyBinder<>), true)]
    public class PropertyBinderInspector: BinderInspector
    {
        private GUIContent[] _pathOptions;
        private string[] _properties;

        protected override void OnEnable()
        {
            base.OnEnable();
            if(Context != null)
                CollectProperties();
        }

        private void CollectProperties()
        {
            var attributes = serializedObject.targetObject.GetPropertyAttributesFrom(Context).ToList();
            _pathOptions = attributes.Select(a => new GUIContent(a.Alias, a.Help)).ToArray();
            _properties = attributes.Select(a => a.Name).ToArray();
        }

        protected override void OnBinding(SerializedProperty path)
        {
            var index = Array.IndexOf(_properties, path.stringValue);
            EditorGUI.BeginChangeCheck();
            index = EditorGUILayout.Popup(new GUIContent("Path"), index, _pathOptions );
            if (!EditorGUI.EndChangeCheck())
                return;

            path.stringValue = index >= 0 ? _properties[index] : string.Empty;
            serializedObject.ApplyModifiedProperties();
            if (Application.isPlaying)
                (target as IBinder)?.Bind();
        }
    }
}