using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Converters;

namespace UIDataBind.Editor.Utils
{
    public static class EditorModelUtils
    {
        private static readonly Type[] AvailableModels;
        private static readonly Dictionary<string, FieldInfo[]> Fields = new Dictionary<string, FieldInfo[]>();

        private static readonly IConverters Converters = new TypeConverters();

        static EditorModelUtils()
        {
            var types = AssemblyTypes().Where(type => !type.IsAbstract).ToList();
            AvailableModels = types.Where(IsInterface<IViewModel>).Where(x=>x.FullName != null).ToArray();
            // ReSharper disable once AssignNullToNotNullAttribute
            foreach (var modelType in AvailableModels)
                Fields.Add(modelType.FullName, modelType.GetFields(BindingFlags.Instance | BindingFlags.Public));

            Converters.Register(types.Where(IsInterface<IPropertyComponent>));
        }

        public static IEnumerable<string> GetModelNames() => AvailableModels.Select(x => x.FullName);


        public static FieldInfo[] GetFields(string modelFullName, Type targetType)
        {
            FieldInfo[] fieldInfos;
            if (!Fields.TryGetValue(modelFullName, out fieldInfos))
                return new FieldInfo[0];

            return targetType != null && fieldInfos.Length > 0
                ? fieldInfos.Where(x => IsTypeValid(x, targetType)).ToArray()
                : fieldInfos;
        }

        private static bool IsTypeValid(FieldInfo x, Type targetType) =>
            targetType == x.FieldType || Converters.Has(x.FieldType, targetType);

        #region Assembly Reflections
        private static IEnumerable<Type> AssemblyTypes()
        {
            var buffer = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    buffer.AddRange(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    buffer.AddRange(ex.Types.Where(type => type != null));
                }
            }
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsInterface<T>(Type type) =>
            !type.IsInterface && type.GetInterface(typeof(T).FullName) != null;
        #endregion
    }
}