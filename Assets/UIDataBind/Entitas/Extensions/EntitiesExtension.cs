using UIDataBind.Base;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitiesExtension
    {
        public static bool IsEventOf(this UiBindEntity entity, BindingPath model, ControlEvent type) =>
            entity.IsChildOf(model) && entity.hasEvent && (entity.@event.Value & type) != 0x0;

        public static bool IsEventOf(this UiBindEntity entity, BindingPath model, BindingPath property, ControlEvent type) =>
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
    }
}