using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UIDataBindCore;
using UIDataBindCore.Attributes;
using UIDataBindCore.Extensions;

namespace Plugins.UIDataBind.Editor.Extensions
{
    public static class ReflectionExtension
    {
        public static IEnumerable<BindAttribute> GetAttributes(this Type contextType, MemberTypes memberTypes,
            Type expectedType = null)
        {
            var members = contextType.GetDataContextType().Members;
            foreach (var member in members)
            {
                if(member.MemberType != memberTypes)
                    continue;

                if(member.CanBeUsedFor(expectedType))
                    yield return GetBindAttribute(member);
            }
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
        public static Type GetPropertyValueType(this Type propertyBinderType) =>
            propertyBinderType?.GetProperty("Value", BindingFlags.Instance | BindingFlags.Public)?.PropertyType;
    }
}