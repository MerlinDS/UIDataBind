using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components
{
    [UiBind]
    public struct StringPropertyComponent: IPropertyComponent<string>, IComponent
    {
        public string Value { get; set; }
        public override string ToString() => $"Str({Value})";
    }
}