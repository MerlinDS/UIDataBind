using Entitas;
using UIDataBind.Base.Components;

namespace UIDataBind.Entitas.Components.Properties
{
    [UiBind]
    public struct StringProperty: IPropertyComponent<string>, IComponent
    {
        public string Value { get; set; }
        public override string ToString() => $"Str({Value})";
    }
}