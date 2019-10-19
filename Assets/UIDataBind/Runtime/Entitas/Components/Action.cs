using Entitas;

namespace UIDataBind.Entitas.Components
{
    public enum ActionType
    {
        Click,
        PointerEnter,
        PointerExit,
        Change
    }

    [UiBind]
    public class Action : IComponent
    {
        public ActionType Type;

        public override string ToString() => Type.ToString();
    }


}