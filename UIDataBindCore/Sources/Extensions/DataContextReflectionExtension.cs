using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UIDataBindCore.Attributes;
using UIDataBindCore.Base;

namespace UIDataBindCore.Extensions
{
    public static class ReflectionExtension
    {
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.NonPublic |
                                                  System.Reflection.BindingFlags.Public |
                                                  System.Reflection.BindingFlags.Instance |
                                                  System.Reflection.BindingFlags.DeclaredOnly;

        private static readonly Type BindAttributeType = typeof(BindAttribute);
        private static readonly Type InitializableType = typeof(IInitializable);
        private static readonly Type BindingPropertyType = typeof(IBindProperty);
        private static readonly Type DataContextType = typeof(IDataContext);

        public static DataContextInfo GetDataContextType(this Type contextType)
        {
            if (contextType == null)
                throw new ArgumentNullException(nameof(contextType));

            if (!typeof(IDataContext).IsAssignableFrom(contextType))
                throw new ArgumentException("Context type must be assignable from IDataContext", nameof(contextType));

            var members = contextType.GetBindMembers();
            return new DataContextInfo
            {
                Name = contextType.Name,
                IsInitializable = InitializableType.IsAssignableFrom(contextType),
                Members = members
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemberInfo[] GetBindMembers(this IReflect type) =>
            type.GetMembers(BindingFlags).Where(Filter).ToArray();

        private static bool Filter(MemberInfo member)
        {
            if (!Attribute.IsDefined(member, BindAttributeType))
                return false;

            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    var fieldInfo = member as FieldInfo;
                    return BindingPropertyType.IsAssignableFrom(fieldInfo?.FieldType)
                           || DataContextType.IsAssignableFrom(fieldInfo?.FieldType);
                case MemberTypes.Method:
                    var methodInfo = (member as MethodInfo);
                    return methodInfo?.ReturnType == typeof(void) && methodInfo.GetParameters().Length == 0;
                default:
                    return false;
            }
        }

        public static DataContextReferences GetReferences(this IDataContext context, DataContextInfo info)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (info.Members == null)
                throw new ArgumentException("DataContextInfo not initialized!", nameof(info));

            var references = new DataContextReferences(context);
            var length = info.Members.Length;
            for (var i = 0; i < length; i++)
                context.GetMemberReference(info.Members[i], references);
            return references;
        }

        private static void GetMemberReference(this IDataContext context, MemberInfo member,
            DataContextReferences references)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                {
                    var value = (member as FieldInfo)?.GetValue(context);
                    if (value is IBindProperty property)
                        references.Properties.Add(member.Name, property);
                    else if (value is IDataContext subContext)
                        references.SubContexts.Add(member.Name, subContext);
                    return;
                }
                case MemberTypes.Method:
                {
                    if (((MethodInfo) member).CreateDelegate(typeof(Action), context) is Action action)
                        references.Methods.Add(member.Name, action);
                    return;
                }
            }
        }
    }
}