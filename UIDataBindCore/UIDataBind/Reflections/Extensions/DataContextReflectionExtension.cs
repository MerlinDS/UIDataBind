using System;
using System.Linq;
using System.Reflection;
using UIDataBindCore.Attributes;

namespace UIDataBindCore.Reflections.Extensions
{
    public static class DataContextReflectionExtension
    {
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.NonPublic |
                                                  System.Reflection.BindingFlags.Public |
                                                  System.Reflection.BindingFlags.Instance |
                                                  System.Reflection.BindingFlags.DeclaredOnly;

        private static readonly Type BindAttributeType = typeof(BindAttribute);
        private static readonly Type InitializableType = typeof(IInitializable);

        public static DataContextInfo GetDataContextType(this IDataContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var type = context.GetType();
            return new DataContextInfo
            {
                Name = type.Name,
                Guid = type.GUID,
                IsInitializable = InitializableType.IsAssignableFrom(type),
                Members = type.GetBindMembers()
            };
        }

        private static MemberInfo[] GetBindMembers(this IReflect type) =>
            type.GetMembers(BindingFlags).Where(f => Attribute.IsDefined(f, BindAttributeType)).ToArray();
    }
}