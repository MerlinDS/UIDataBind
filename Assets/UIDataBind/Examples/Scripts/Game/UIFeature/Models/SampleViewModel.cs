using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    public struct SampleViewModel : IViewModel
    {
        public bool Visible;

        public bool Toggle;
        public bool Clicked;
        public bool ColorClicked;
        public bool Hovered;

        public int ClickedCount;
        public int ToggledCount;

        public Color Color;

        public string Label;
        public string Icon;
        public string HoveringAction;

        public void Update(IProperties properties)
        {
            properties.ReadProperty(nameof(Clicked), ref Clicked);
            properties.ReadProperty(nameof(Hovered), ref Hovered);

            properties.ReadProperty(nameof(ClickedCount), ref ClickedCount);
            properties.ReadProperty(nameof(ColorClicked), ref ColorClicked);
            properties.ReadProperty(nameof(HoveringAction), ref HoveringAction);

            properties.ReadProperty(nameof(Visible), ref Visible);
            properties.ReadProperty(nameof(Label), ref Label);
            properties.ReadProperty(nameof(Toggle), ref Toggle);
            properties.ReadProperty(nameof(ToggledCount), ref ToggledCount);
            properties.ReadProperty(nameof(Icon), ref Icon);
            properties.ReadProperty(nameof(Color), ref Color);
        }

        public void Fetch(IProperties properties)
        {
            properties.WriteProperty(nameof(Clicked), Clicked);
            properties.WriteProperty(nameof(Hovered), Hovered);
            properties.WriteProperty(nameof(ColorClicked), ColorClicked);

            properties.WriteProperty(nameof(ClickedCount), ClickedCount);
            properties.WriteProperty(nameof(HoveringAction), HoveringAction);

            properties.WriteProperty(nameof(Visible), Visible);
            properties.WriteProperty(nameof(Label), Label);
            properties.WriteProperty(nameof(Toggle), Toggle);
            properties.WriteProperty(nameof(ToggledCount), ToggledCount);
            properties.WriteProperty(nameof(Icon), Icon);
            properties.WriteProperty(nameof(Color), Color);
        }
    }
}