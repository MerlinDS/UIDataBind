using System;
using System.Linq;
using System.Reflection;
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
        private string[] _properties;
        private GUIContent[] _pathOptions;

        private SerializedProperty _value;
        private PropertyInfo _valueProperty;
        private Action _resetMethod;
        private bool _hideValue;

        protected override void OnEnable()
        {
            _value = serializedObject.FindProperty("_value");
            _hideValue = serializedObject.HasHideBinderValueAttribute();
            AddExcludedProperties(_value);
            base.OnEnable();

            _resetMethod = serializedObject.GetPropertyBindingResetMethod();
            if(Context != null)
                CollectProperties();
        }

        private void CollectProperties()
        {
            var attributes = serializedObject.GetPropertyAttributesFrom(Context).ToList();
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

        protected override void OnGUI()
        {
            if(_hideValue)
                return;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_value);
            if (!EditorGUI.EndChangeCheck() || !Application.isPlaying)
                return;

            serializedObject.ApplyModifiedProperties();
            _resetMethod.Invoke();
        }
    }
}