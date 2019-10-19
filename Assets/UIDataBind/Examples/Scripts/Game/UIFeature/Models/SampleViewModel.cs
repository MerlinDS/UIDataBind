using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    public struct SampleViewModel : IViewModel
    {
        public int Index;
        public bool Visible;
        public bool Toggle;
        public string Label;
        public string ButtonLabel;
        public string Icon;

        public bool Clicked;
        public bool Hovered;

        public void Update(IProperties properties)
        {
            properties.ReadProperty(nameof(Clicked), ref Clicked);
            properties.ReadProperty(nameof(Hovered), ref Hovered);

            properties.ReadProperty(nameof(Visible), ref Visible);
            properties.ReadProperty(nameof(Label), ref Label);
            properties.ReadProperty(nameof(Toggle), ref Toggle);
            properties.ReadProperty(nameof(Index), ref Index);
            properties.ReadProperty(nameof(ButtonLabel), ref ButtonLabel);
            properties.ReadProperty(nameof(Icon), ref Icon);
        }

        public void Fetch(IProperties properties)
        {
            properties.WriteProperty(nameof(Clicked), Clicked);
            properties.WriteProperty(nameof(Hovered), Hovered);

            properties.WriteProperty(nameof(Visible), Visible);
            properties.WriteProperty(nameof(Label), Label);
            properties.WriteProperty(nameof(Toggle), Toggle);
            properties.WriteProperty(nameof(Index), Index);
            properties.WriteProperty(nameof(ButtonLabel), ButtonLabel);
            properties.WriteProperty(nameof(Icon), Icon);
        }
    }
}