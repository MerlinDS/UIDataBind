using Entitas;
using UIDataBind.Base;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class BinderComponent : IComponent
    {
        public IBinder Value;

        public override string ToString() => $"Binder({Value})";
    }
}