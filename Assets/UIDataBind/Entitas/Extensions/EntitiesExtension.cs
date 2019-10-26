using UIDataBind.Base;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitiesExtension
    {
        public static bool IsEventOf<TModel>(this UiBindEntity entity, ControlEvent type)
            where TModel : struct, IViewModel => entity.IsEventOf(typeof(TModel).Name, type);
        public static bool IsEventOf(this UiBindEntity entity, BindingPath model, ControlEvent type) =>
            entity.IsChildOf(model) && entity.hasEvent && (entity.@event.Value & type) != 0x0;

        public static bool IsEventOf<TModel>(this UiBindEntity entity, BindingPath property, ControlEvent type)
            where TModel : struct, IViewModel => entity.IsEventOf(typeof(TModel).Name, property, type);
        public static bool IsEventOf(this UiBindEntity entity, BindingPath model, BindingPath property,
            ControlEvent type) =>
            entity.hasBinderPath && entity.binderPath.Value == BindingPath.BuildFrom(model, property)
                                 && entity.hasEvent && (entity.@event.Value & type) != 0x0;

        public static bool IsChildOf(this UiBindEntity entity, BindingPath model)
        {
            if (entity.hasBinderPath)
                return BindingPath.GetParent(entity.binderPath.Value) == model;
            if (entity.hasModelPath)
                return BindingPath.GetParent(entity.modelPath.Value) == model;
            return false;
        }

        public static bool IsModel<TModel>(this UiBindEntity entity) where TModel : struct, IViewModel =>
            entity.IsModel(typeof(TModel).Name);

        public static bool IsModel(this UiBindEntity entity, BindingPath path) =>
            entity.hasModelPath && entity.modelPath.Value == path;
    }
}