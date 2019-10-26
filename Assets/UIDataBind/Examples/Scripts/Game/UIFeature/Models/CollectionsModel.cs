using UIDataBind.Base;
using UIDataBind.Binders.Attributes;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    [ViewModel]
    public struct CollectionsModel: IViewModel
    {
        public static readonly BindingPath Path = BindingPath.BuildFrom(nameof(CollectionsModel));

        public bool Visible;
        public string Info;

        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(Visible), ref Visible);

            properties.Refresh(nameof(Info), ref Info);
        }
    }
}