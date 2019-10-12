using UIDataBind.Base;
using UIDataBind.Entitas.Wrappers;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntityProviderExtension
    {

        private static UiBindContext Context => Contexts.sharedInstance.uiBind;

        public static void Destroy(this IEntityProvider provider) =>
            provider.EntityManager.DestroyEntity(provider.Entity);


        public static IEntityProvider GetEngineProvider(this IBinder binder, string path)
        {
            var manger = new EntitasEntityManager(Context);//TODO: Get existing one
            var entity = (UiBindEntity)manger.CreateEntity();
            entity.AddBinder(binder);
            entity.AddBindingPath(path);
            entity.isView = binder is IView;
            entity.isDirty = true;

            return new EntitasProvider(entity, manger);
        }

        public static void AddPropertyComponent<TValue>(this IEntityProvider provider, TValue defaultValue) =>
            provider.EntityManager.AddComponent(provider.Entity, defaultValue);

        public static TValue GetPropertyValue<TValue>(this IEntityProvider provider) =>
            provider.EntityManager.GetComponentData<TValue>(provider.Entity);

    }
}