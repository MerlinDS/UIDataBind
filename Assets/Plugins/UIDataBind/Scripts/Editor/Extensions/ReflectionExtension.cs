using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Plugins.UIDataBind.Attributes;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;
using UnityEditor;

namespace Plugins.UIDataBind.Editor.Extensions
{
    public static class ReflectionExtension
    {
        public static bool HasHideBinderValueAttribute(this SerializedObject serializedObject) =>
           Attribute.IsDefined(serializedObject.targetObject.GetType(), typeof(HideBinderValueAttribute));

        [NotNull]
        public static Action GetPropertyBindingResetMethod(this SerializedObject serializedObject)
        {
            var propertyBinder = serializedObject.targetObject;
            var methodInfo = propertyBinder.GetType().GetMethod("ResetValue", BindingFlags.Instance | BindingFlags.Public);
            if (methodInfo != null && methodInfo.CreateDelegate(typeof(Action), propertyBinder) is Action action)
                return action;
            return () => { };
        }
        public static IEnumerable<BindAttribute> GetPropertyAttributesFrom(this SerializedObject serializedObject, IDataContext context)
        {
            var binderType = serializedObject.targetObject.GetType().GetPropertyValueType();
            var members = context.GetType().GetDataContextType().Members;

            return members.Where(member => member.MemberType == MemberTypes.Field && member.CanBeUsedFor(binderType))
                .Select(GetBindAttribute);
        }

        private static bool CanBeUsedFor(this MemberInfo member, Type expectedType)
        {
            var memberGenericType = member.GetGenericType();
            return memberGenericType == expectedType ||
                   BindingKernel.Instance.ConversionMethods.Has(memberGenericType, expectedType);
        }

        [CanBeNull]
        private static Type GetGenericType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                {
                    var fieldType = (member as FieldInfo)?.FieldType;
                    var @interface = fieldType?.GetInterface(typeof(IBindProperty<>).Name);
                    return @interface?.GenericTypeArguments.FirstOrDefault();
                }
                default:
                    return null;
            }
        }

        [NotNull]
        private static BindAttribute GetBindAttribute(MemberInfo member)
        {
            var attribute = (BindAttribute) Attribute.GetCustomAttribute(member, typeof(BindAttribute), true);
            var alias = string.IsNullOrEmpty(attribute.Alias) ? member.Name.ConvertToHumanReadtable() : attribute.Alias;
            return new BindAttribute(alias, attribute.Help) {Name = member.Name};
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type GetPropertyValueType(this Type propertyBinderType) =>
            propertyBinderType?.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public)?.PropertyType;
    }
}