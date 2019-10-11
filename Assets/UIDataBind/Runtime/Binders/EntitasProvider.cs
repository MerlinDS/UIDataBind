using Entitas;
using UIDataBind.Base;
using UIDataBind.Base.Components;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Binders
{
    public class EntitasProvider : IEntityProvider
    {
        public IEntity Entity;
        public int PropertyComponentIndex;
    }

    public interface IEntityProvider
    {
    }

    public static class EntityProviderExtension
    {

        private static UiBindContext Context => Contexts.sharedInstance.uiBind;

        public static void Destroy(this IEntityProvider provider) =>
            ((EntitasProvider) provider).Entity.Destroy();


        public static IEntityProvider GetEngineProvider(this IBinder binder, string path)
        {
            var entity = Context.CreateEntity();
            entity.AddBinder(binder);
            entity.AddBindingPath(path);
            entity.isView = binder is IView;
            entity.isDirty = true;


            return new EntitasProvider(){Entity = entity};
        }

        public static void AddPropertyComponent<TValue>(this IEntityProvider provider, TValue defaultValue) =>
            ((EntitasProvider) provider).AddPropertyComponent(defaultValue);

        private static void AddPropertyComponent<TValue>(this EntitasProvider provider, TValue defaultValue)
        {
            Context.AddPropertyComponent(provider.Entity, defaultValue);
            provider.PropertyComponentIndex = Context.GetPropertyComponentIndex<TValue>();
        }

        public static TValue GetPropertyValue<TValue>(this IEntityProvider provider) =>
            ((EntitasProvider) provider).GetPropertyValue<TValue>();

        private static TValue GetPropertyValue<TValue>(this EntitasProvider provider)
        {
            if (provider.PropertyComponentIndex < 0)
                return default;

            var component = (IPropertyComponent<TValue>) provider.Entity.GetComponent(provider.PropertyComponentIndex);
            return component.Value;
        }
    }
}