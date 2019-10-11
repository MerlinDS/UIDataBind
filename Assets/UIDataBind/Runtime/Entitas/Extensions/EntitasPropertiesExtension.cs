using UIDataBind.Base;
using UIDataBind.Entitas.Wrappers;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasPropertiesExtension
    {
        public static IProperties GetProperties(this UiBindContext context, string modelPath)
        {
            var properties = new EntitasContextWrapper(context, modelPath);
            var entity = context.GetEntity<UiBindEntity>(modelPath, true);
            entity.isModel = true;
            return properties;
        }

        public static TEntity GetEntity<TEntity>(this UiBindContext context, string path, bool createIfNull)
            where TEntity : class
        {
            var entity = context.GetEntityWithPath(path);
            if (!createIfNull || entity != null)
                return entity as TEntity;

            entity = context.CreateEntity();
            entity.AddPath(path);
            return entity as TEntity;
        }

        public static void UpdateModel<TViewModel>(this IProperties properties, ref TViewModel model)
            where TViewModel : IViewModel => model.Update(properties);

        public static void Fetch<TViewModel>(this IProperties properties, TViewModel model)
            where TViewModel : IViewModel => model.Fetch(properties);

        public static void ReadProperty(this IProperties properties, string propertyName, ref bool value)
        {
            var entity = properties.GetPropertyEntity<UiBindEntity>(propertyName);
            if (entity?.hasBooleanProperty ?? false)
                value = entity.booleanProperty.Value;
        }

        public static void WriteProperty(this IProperties properties, string propertyName, bool value)
        {
            var entity = properties.GetPropertyEntity<UiBindEntity>(propertyName, true);
            if (!entity.hasBooleanProperty)
            {
                entity.AddBooleanProperty(value);
                entity.isProperty = true;
                entity.isDirty = true;
                return;
            }

            if (entity.booleanProperty.Value == value)
                return;

            entity.ReplaceBooleanProperty(value);
            entity.isDirty = true;
        }
    }
}