using UIDataBind.Base;
using UIDataBind.Entitas.Components;
using UIDataBind.Entitas.Wrappers;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntityProviderExtension
    {

        private static UiBindContext Context => Contexts.sharedInstance.uiBind;

        public static void Destroy(this IEntityProvider provider) =>
            provider.EntityManager.DestroyEntity(provider.Entity);


        /*public static IEntityProvider GetEngineProvider(this IOldBinder binder)
        {
            var manger = new EntitasEntityManager(Context);//TODO: Get existing one
            var entity = (UiBindEntity)manger.CreateEntity();
//            entity.AddBinder(binder);
            entity.AddBindingPath(binder.Path);
            entity.isView = binder is IView;

            return new EntitasProvider(entity, manger);
        }*/

        /*public static void CreateAction(this IOldBinder binder, ActionType type)
        {
            var actionEntity = (UiBindEntity)binder.Engine.EntityManager.CreateEntity();
//            actionEntity.AddBindingPath(binder.Path);
            actionEntity.AddAction(type);
            actionEntity.isDirty = true;
        }*/

        /*public static void UpdateValue<TValue>(this IOldBinder binder, TValue value, bool createChangeAction = false)
        {
            binder.Engine.EntityManager.SetComponentData(binder.Engine.Entity, value);
            ((UiBindEntity) binder.Engine.Entity).isDirty = true;
            if(createChangeAction)
                binder.CreateAction(ActionType.Change);
        }*/

        public static void AddPropertyComponent<TValue>(this IEntityProvider provider, TValue defaultValue) =>
            provider.EntityManager.AddComponent(provider.Entity, defaultValue);

        public static TValue GetPropertyValue<TValue>(this IEntityProvider provider) =>
            provider.EntityManager.GetComponentData<TValue>(provider.Entity);

    }
}