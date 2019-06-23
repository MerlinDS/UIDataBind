using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Plugins.UIDataBind.Attributes;

namespace Plugins.UIDataBind.Editor.Utils
{
    public static class ReflectionExtension
    {
        public static IEnumerable<BindingPropertyAttribute> GetBindingPropertyAttributes(
            [NotNull] this Type contextType)
        {
            var attributeType = typeof(BindingPropertyAttribute);
            var fieldInfos = contextType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var fieldInfo in fieldInfos)
            {
                if (!Attribute.IsDefined(fieldInfo, attributeType))
                    continue;

                var attribute = (BindingPropertyAttribute) Attribute.GetCustomAttribute(fieldInfo, attributeType, true);
                if (attribute == null)
                    continue;

                var bindingName = attribute.BindingName;
                if (string.IsNullOrEmpty(bindingName))
                    bindingName = fieldInfo.Name.ConvertToHumanReadtable();

                attribute = new BindingPropertyAttribute(bindingName, attribute.AllowConversation)
                    {Name = fieldInfo.Name};

                yield return attribute;
            }
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