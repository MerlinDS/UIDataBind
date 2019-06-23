using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Editor.Utils
{
    public static class ReflectionExtension
    {
        public static IEnumerable<FieldInfo> GetBindingPropertiesInfo([NotNull] this Type contextType, [NotNull] Type bindingType)
        {
            return contextType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.FieldType.IsAssignableFrom<IBindingProperty>())
                .Where(f => f.IsFieldGenericTypeEqualsTo(bindingType));
        }

        public static bool IsFieldGenericTypeEqualsTo(this FieldInfo field, Type type) =>
            field.FieldType.GetFirstGenericTypeFrom(typeof(IBindingProperty<>)) == type;

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