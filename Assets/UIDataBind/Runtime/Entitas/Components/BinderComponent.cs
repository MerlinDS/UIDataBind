using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BinderComponent : IComponent
    {
        public IBinder Value;

        public override string ToString() => $"{Value}";
    }

    public static class BinderComponentExtension
    {
        public static bool HasValueBinder(this UiBindEntity entity) =>
            entity.hasBinder && entity.binder.Value is IValueBinder;
        public static IValueBinder AsValueBinder(this UiBindEntity entity) =>
            entity.hasBinder ? entity.binder.Value as IValueBinder : null;


    }
}