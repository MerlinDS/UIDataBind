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

        public void Update(IProperties properties)
        {
            properties.ReadProperty(nameof(Visible), ref Visible);
            properties.ReadProperty(nameof(Label), ref Label);
            properties.ReadProperty(nameof(Toggle), ref Toggle);
            properties.ReadProperty(nameof(Index), ref Index);
            properties.ReadProperty(nameof(ButtonLabel), ref ButtonLabel);
        }

        public void Fetch(IProperties properties)
        {
            properties.WriteProperty(nameof(Visible), Visible);
            properties.WriteProperty(nameof(Label), Label);
            properties.WriteProperty(nameof(Toggle), Toggle);
            properties.WriteProperty(nameof(Index), Index);
            properties.WriteProperty(nameof(ButtonLabel), ButtonLabel);
        }
    }
}