using UIDataBind.Base;
using UIDataBind.Entitas.Extensions;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    public struct SampleViewModel : IViewModel
    {
        public bool Visible;
        public void Update(IProperties properties)
        {
            properties.ReadProperty(nameof(Visible), ref Visible);
        }

        public void Fetch(IProperties properties)
        {
            properties.WriteProperty(nameof(Visible), Visible);
        }
    }
}