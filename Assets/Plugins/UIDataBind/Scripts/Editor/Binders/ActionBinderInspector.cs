using System;
using System.Linq;
using Plugins.UIDataBind.Binders;
using Plugins.UIDataBind.Editor.Extensions;
using UIDataBindCore;
using UnityEditor;
using UnityEngine;

namespace Plugins.UIDataBind.Editor.Binders
{
    [CustomEditor(typeof(ActionBinder), true)]
    public class ActionBinderInspector : BinderInspector
    {
        private GUIContent[] _pathOptions;
        private string[] _methods;

        protected override void OnEnable()
        {
            base.OnEnable();
            if(Context != null)
                CollectMethods();
        }

        private void CollectMethods()
        {
            var attributes = serializedObject.GetMethodAttributesFrom(Context).ToList();
            _pathOptions = attributes.Select(a => new GUIContent(a.Alias, a.Help)).ToArray();
            _methods = attributes.Select(a => a.Name).ToArray();
        }

        protected override void OnBinding(SerializedProperty path)
        {
            var index = Array.IndexOf(_methods, path.stringValue);
            EditorGUI.BeginChangeCheck();
            index = EditorGUILayout.Popup(new GUIContent("Path"), index, _pathOptions );
            if (!EditorGUI.EndChangeCheck())
                return;

            path.stringValue = index >= 0 ? _methods[index] : string.Empty;
            serializedObject.ApplyModifiedProperties();
            if (Application.isPlaying)
                (target as IBinder)?.Bind();
        }
    }
}