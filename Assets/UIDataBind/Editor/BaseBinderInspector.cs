using System;
using System.Linq;
using UIDataBind.Base;
using UIDataBind.Binders;
using UIDataBind.Binders.Attributes;
using UIDataBind.Binders.Extensions;
using UIDataBind.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace UIDataBind.Editor
{
    [CustomEditor(typeof(BaseBinder), true)]
    public class BaseBinderInspector : UnityEditor.Editor
    {
        private SerializedProperty _type;
        private SerializedProperty _path;
        private SerializedProperty _bindingType;

        private bool _isView;
        private bool _isChild;
        private Type _valueType;
        protected BindingType BindingType => (BindingType) _bindingType.enumValueIndex;

        protected virtual void OnEnable()
        {
            _type = serializedObject.FindProperty("_type");
            _path = serializedObject.FindProperty("_path");
            _bindingType = serializedObject.FindProperty("_bindingType");

            if (target is IValueBinder binder)
                _valueType = binder.ValueType;
            _isView = target is IView;
            _isChild = _isView && ((BaseBinder) target).GetParentView() != null;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (_isView)
                _type.stringValue = BinderGUILayout.Popup(_type.stringValue, EditorModelUtils.GetModelNames());

            DrawBindingType();

            if (_isView || BindingType == BindingType.Absolute)
                DrawPathAsText(_path, HelpBoxType.EmptyPathWarning);
            else if (BindingType == BindingType.Context)
                DrawPath();


            serializedObject.ApplyModifiedProperties();
            serializedObject.DoDrawDefaultInspector();
        }

        private void DrawBindingType()
        {
            if (_isView && !_isChild)
            {
                _bindingType.enumValueIndex = (int) BindingType.Context;
                return;
            }
            var names = _bindingType.enumNames;
            var currentName = names[_bindingType.enumValueIndex];
            var displayOptions =
                _isView
                    ? names.Where(x => x != BindingType.Self.ToString()).ToArray()
                    : names;

            var selectedIndex = Array.IndexOf(displayOptions, currentName);
            selectedIndex = EditorGUILayout.Popup(_type.displayName, selectedIndex, displayOptions);
            var selectedOption = displayOptions[selectedIndex];
            _bindingType.enumValueIndex = Array.IndexOf(names, selectedOption);
        }

        private void DrawPath()
        {
            if (string.IsNullOrEmpty(FullModelName))
            {
                DrawHelpBox(HelpBoxType.NoModelError);
                return;
            }

            var fieldInfos = EditorModelUtils.GetFields(FullModelName, _valueType);
            if (fieldInfos.Length == 0)
            {
                DrawHelpBox(HelpBoxType.NoPropertiesError);
                DrawPathAsText(_path, HelpBoxType.AddPropertyInfo);
                return;
            }

            EditorGUILayout.BeginHorizontal();
            _path.stringValue = BinderGUILayout.Popup(_path.displayName, _path.stringValue, fieldInfos.Select(x => x.Name));
            EditorGUILayout.EndHorizontal();
        }

        private void DrawPathAsText(SerializedProperty property, HelpBoxType helpBoxType)
        {
            EditorGUILayout.PropertyField(property);
            if (string.IsNullOrEmpty(property.stringValue) && helpBoxType == HelpBoxType.EmptyPathWarning)
                DrawHelpBox(HelpBoxType.EmptyPathWarning);
            if (!string.IsNullOrEmpty(property.stringValue) && helpBoxType == HelpBoxType.AddPropertyInfo)
                DrawHelpBox(HelpBoxType.AddPropertyInfo);
        }

        private enum HelpBoxType
        {
            NoModelError,
            NoPropertiesError,
            AddPropertyInfo,
            EmptyPathWarning
        }

        private void DrawHelpBox(HelpBoxType type)
        {
            string message;
            MessageType messageType;
            switch (type)
            {
                case HelpBoxType.NoModelError:
                    message = "Has no available model to bind!";
                    messageType = MessageType.Error;
                    break;
                case HelpBoxType.NoPropertiesError:
                    message = $"{FullModelName}\nHas no available properties for {target.GetType().Name}!";
                    messageType = MessageType.Error;
                    break;
                case HelpBoxType.AddPropertyInfo:
                    message = $"Add a \"{_path.stringValue}\" field with appropriate type to the model!";
                    messageType = MessageType.Info;
                    break;
                case HelpBoxType.EmptyPathWarning:
                    message = $"{_path.displayName} is empty!";
                    messageType = MessageType.Warning;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            EditorGUILayout.HelpBox(message, messageType);
        }

        private string FullModelName
        {
            get
            {
                if (_isView)
                    return _type.stringValue;

                var parentView = ((BaseBinder) target).GetParentView() as Component;
                return parentView == null
                    ? string.Empty
                    : new SerializedObject(parentView).FindProperty("_type")?.stringValue;
            }
        }
    }
}