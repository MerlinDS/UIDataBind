using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Plugins.UIDataBind.Attributes;
using Plugins.UIDataBind.Properties;

namespace Plugins.UIDataBind.Extensions
{
    public static class ReflectionsExtension
    {
        private const BindingFlags MethodFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance |
                                                 BindingFlags.DeclaredOnly;

        private const BindingFlags PropertyFlags = BindingFlags.Instance | BindingFlags.NonPublic;

        private static readonly Type BindingMethodAttribute = typeof(BindingMethodAttribute);
        private static readonly Type BindingPropertyAttribute = typeof(BindingPropertyAttribute);
        private static readonly Type BindingPropertyType = typeof(IBindingProperty);

        public static IEnumerable<Tuple<string, object>> GetBindingProperties(this IViewContext context) =>
            context.GetType().GetBindingPropertyInfos().Select(field => CreatePair(field, context));

        public static IEnumerable<FieldInfo> GetBindingPropertyInfos(this Type contextType) =>
            contextType.GetFields(PropertyFlags)
                .Where(field => Attribute.IsDefined(field, BindingPropertyAttribute))
                .Where(field => BindingPropertyType.IsAssignableFrom(field.FieldType));

        public static IEnumerable<Tuple<string, object>> GetBindingMethods(this IViewContext context) =>
            context.GetType().GetBindingMethodInfos().Select(method =>CreatePair(method, context));

        public static IEnumerable<MethodInfo> GetBindingMethodInfos(this Type contextType) =>
            contextType.GetMethods(MethodFlags)
                .Where(method => Attribute.IsDefined(method, BindingMethodAttribute))
                .Where(method => method.GetParameters().Length == 0)
                .Where(method => method.ReturnType == typeof(void));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<string, object> CreatePair(FieldInfo fieldInfo, object target) =>
            new Tuple<string, object>(fieldInfo.Name, (IBindingProperty) fieldInfo.GetValue(target));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Tuple<string, object> CreatePair(MethodInfo methodInfo, object target) =>
            new Tuple<string, object>(methodInfo.Name, (Action) methodInfo.CreateDelegate(typeof(Action), target));
    }
}