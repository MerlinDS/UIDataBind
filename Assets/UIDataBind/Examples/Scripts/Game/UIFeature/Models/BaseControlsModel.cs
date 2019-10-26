using UIDataBind.Base;
using UIDataBind.Binders.Attributes;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    [ViewModel]
    public struct BaseControlsModel: IViewModel
    {
        public static readonly BindingPath Path = BindingPath.BuildFrom(nameof(BaseControlsModel));

        public bool Visible;

        public string Info;
        public string SampleText;
        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(Visible), ref Visible);

            properties.Refresh(nameof(Info), ref Info);
            properties.Refresh(nameof(SampleText), ref SampleText);
        }
    }
}