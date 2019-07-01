using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Extensions;
using Plugins.UIDataBind.Properties;
using Plugins.UIDataBind.Utils;

namespace Plugins.UIDataBind.Editor.Utils
{
    public static class EditorReflectionExtension
    {
        public static IEnumerable<BindingPropertyAttribute> GetBindingPropertyAttributes(
            [NotNull] this Type contextType, Type expectedType = null)
        {
            var fieldInfos = contextType.GetBindingPropertyInfos();
            if (expectedType == null)
                return fieldInfos.GetBindingAttributes<BindingPropertyAttribute>();


            return fieldInfos.GetBindingAttributes<BindingPropertyAttribute>(memberInfo =>
            {
                var filedType = ((FieldInfo) memberInfo).FieldType;
                var propertyType = filedType.GetFirstGenericTypeFrom(typeof(IBindingProperty<>));
                return propertyType == expectedType || ConvertUtils.IsConvertible(propertyType, expectedType);
            });
        }

        public static IEnumerable<BindingMethodAttribute> GetBindingMethodAttributes([NotNull] this Type contextType) =>
            contextType.GetBindingMethodInfos().GetBindingAttributes<BindingMethodAttribute>();

        private static IEnumerable<TAttribute> GetBindingAttributes<TAttribute>(
            this IEnumerable<MemberInfo> memberInfos,
            Func<MemberInfo, bool> filter = null)
            where TAttribute : BaseBindingAttribute, new()
        {
            var attributeType = typeof(TAttribute);
            foreach (var memberInfo in memberInfos)
            {
                var attribute = (TAttribute) Attribute.GetCustomAttribute(memberInfo, attributeType, true);
                if (attribute == null)
                    continue;

                if (filter == null || filter.Invoke(memberInfo))
                    yield return attribute.CopyAttribute(memberInfo);
            }
        }

        private static TAttribute CopyAttribute<TAttribute>(this TAttribute attribute, MemberInfo memberInfo)
            where TAttribute : BaseBindingAttribute, new()
        {
            var bindingName = attribute.BindingName;
            if (string.IsNullOrEmpty(bindingName))
                bindingName = memberInfo.Name.ConvertToHumanReadtable();

            attribute = new TAttribute
            {
                Name = memberInfo.Name,
                BindingName = bindingName
            };

            return attribute;
        }

        #region Global

        /// <summary>
        /// Get first generic type argument if specified interfaces that searchType from derived
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static Type GetFirstGenericTypeFrom([NotNull] this Type searchType, [NotNull] Type interfaceType)
        {
            if (!interfaceType.IsInterface)
                throw new ArgumentException($"{interfaceType.Name} must be an interface", nameof(interfaceType));

            if (string.IsNullOrEmpty(interfaceType.FullName))
                throw new ArgumentException("Could not get the name of interface", nameof(interfaceType));

            var @interface = searchType.GetInterface(interfaceType.FullName, true);
            return @interface.GenericTypeArguments.First();
        }

        /// <summary>
        /// Syntax sugar for <see cref="System.Type.IsAssignableFrom"/>
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public static bool IsAssignableFrom<TInterface>([NotNull] this Type type) =>
            typeof(TInterface).IsAssignableFrom(type);

        #endregion
    }
}