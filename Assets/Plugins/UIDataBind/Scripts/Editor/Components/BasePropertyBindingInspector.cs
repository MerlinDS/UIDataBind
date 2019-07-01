using System;
using System.Collections.Generic;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Base;
using Plugins.UIDataBind.Components;
using Plugins.UIDataBind.Editor.Utils;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Components
{
    [CustomEditor(typeof(BasePropertyBinding<>), true)]
    public sealed class BasePropertyBindingInspector : BaseBindingInspector
    {
        private SerializedProperty _defaultValue;

        private bool _isValueDrawable = true;
        private bool _isAlwaysShowValue;


        protected override void OnEnable()
        {
            base.OnEnable();
            _isValueDrawable = !Attribute.IsDefined(Binding.GetType(), typeof(HideBindingValueAttribute));
            if(_isValueDrawable)
                _isAlwaysShowValue = Attribute.IsDefined(Binding.GetType(), typeof(ShowBindingValueAttribute));

            _defaultValue = serializedObject.FindProperty("_value");
            ExcludingProperties = new[] {ExcludingProperties[0], _defaultValue};
        }

        protected override IEnumerable<BaseBindingAttribute> GetAttributes(IViewContext context)
        {
            var expectedType = ((IPropertyBindingBehaviour)Binding).GetValueType;
            return context.GetType().GetBindingPropertyAttributes(expectedType);
        }

        protected override void OnInternalGUI()
        {
            if (!_isAlwaysShowValue && BindingType != BindingType.None)
                return;

            if(!_isValueDrawable)
                return;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(_defaultValue);

            if (!EditorGUI.EndChangeCheck())
                return;

            serializedObject.ApplyModifiedProperties();
            Binding.Reactivate();

        }
    }
}