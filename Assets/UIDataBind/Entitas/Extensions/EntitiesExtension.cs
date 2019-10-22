using UIDataBind.Base;
using UIDataBind.Utils.Extensions;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitiesExtension
    {
        public static bool IsEventOf(this UiBindEntity entity, OldBindingPath model, ControlEvent type) =>
            entity.IsChildOf(model) && entity.hasEvent && (entity.@event.Value & type) != 0x0;

        public static bool IsEventOf(this UiBindEntity entity, OldBindingPath model, OldBindingPath property, ControlEvent type) =>
            entity.hasBinderPath && entity.binderPath.Value == model.BuildPath(property)
                                  && entity.hasEvent && (entity.@event.Value & type) != 0x0;

        public static bool IsChildOf(this UiBindEntity entity, OldBindingPath model) =>
            entity.hasParentModel && entity.parentModel.Path == model;

    }
}