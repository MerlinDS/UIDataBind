using Entitas;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public class View : IComponent
    {
        public override string ToString() => nameof(View);
    }
}