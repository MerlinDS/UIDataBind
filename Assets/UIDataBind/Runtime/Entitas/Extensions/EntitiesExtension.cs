using UIDataBind.Base;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitiesExtension
    {
        public static bool IsEventOf(this UiBindEntity entity, BindingPath model, UIEventType type) =>
            entity.IsChildOf(model) && entity.hasEvent && (entity.@event.Value & type) != 0x0;

        public static bool IsChildOf(this UiBindEntity entity, BindingPath model) =>
            entity.hasParentModel && entity.parentModel.Path == model;

    }
}