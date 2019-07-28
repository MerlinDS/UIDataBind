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

        public static DataContextInfo GetDataContextType(this Type contextType)
        {
            if (contextType == null)
                throw new ArgumentNullException(nameof(contextType));

            if(!typeof(IDataContext).IsAssignableFrom(contextType))
                throw new ArgumentException("Context type must be assignable from IDataContext", nameof(contextType));

            return new DataContextInfo
            {
                Name = contextType.Name,
                Guid = contextType.GUID,
                IsInitializable = InitializableType.IsAssignableFrom(contextType),
                Members = contextType.GetBindMembers()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static MemberInfo[] GetBindMembers(this IReflect type) =>
            type.GetMembers(BindingFlags).Where(f => Attribute.IsDefined((MemberInfo) f, BindAttributeType)).ToArray();

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
                    if ((member as FieldInfo)?.GetValue(context) is IBindProperty property)
                        references.Properties.Add(member.Name, property);
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