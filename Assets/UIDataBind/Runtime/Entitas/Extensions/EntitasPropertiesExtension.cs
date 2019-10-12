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


        public static void ReadProperty<TValue>(this IProperties properties, string propertyName, ref TValue value)
        {
            var entity = properties.GetPropertyEntity<IUiBindEntity>(propertyName);
            if (entity == null || !properties.EntityManager.HasComponent<TValue>(entity))
                return;

            value = properties.EntityManager.GetComponentData<TValue>(entity);
        }

        #region Write

        public static void WriteProperty<TValue>(this IProperties properties, string propertyName, TValue value)
        {
            var entity = properties.GetPropertyEntity<IUiBindEntity>(propertyName, true);

            if (!properties.EntityManager.HasComponent<TValue>(entity))
            {
                properties.EntityManager.AddComponent(entity, value);
                ((UiBindEntity)entity).isProperty = true;
                ((UiBindEntity)entity).isDirty = true;
                return;
            }

            var previousValue = properties.EntityManager.GetComponentData<TValue>(entity);
            if(Equals(previousValue, value))
                return;

            properties.EntityManager.SetComponentData(entity, value);
            ((UiBindEntity)entity).isDirty = true;
        }
        #endregion
    }
}