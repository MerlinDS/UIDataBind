using UIDataBind.Base;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    public struct ControlsViewModel : IViewModel
    {
        public static readonly BindingPath Path = BindingPath.BuildFrom("ChildView");

        public bool Visible;
        public int Index;
        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(Visible), ref Visible);
            properties.Refresh(nameof(Index), ref Index);
        }
    }
}