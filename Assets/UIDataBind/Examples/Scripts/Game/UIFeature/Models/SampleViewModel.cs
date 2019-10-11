using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    public struct SampleViewModel : IViewModel
    {
        public bool Visible;
        public int Index;
        public void Update(IProperties properties)
        {
            properties.ReadProperty(nameof(Visible), ref Visible);
            properties.ReadProperty(nameof(Index), ref Index);
        }

        public void Fetch(IProperties properties)
        {
            properties.WriteProperty(nameof(Visible), Visible);
            properties.WriteProperty(nameof(Index), Index);
        }
    }
}