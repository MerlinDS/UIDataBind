using UIDataBind.Base;
using UIDataBind.Binders.Attributes;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    [ViewModel]
    public struct BaseControlsModel: IViewModel
    {
        public bool Visible;

        public int Integer;

        public ControlEvent ChangeInteger;

        public string Info;
        public string SampleText;

        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(Visible), ref Visible);

            properties.Refresh(nameof(Integer), ref Integer);
            properties.Refresh(nameof(ChangeInteger), ref ChangeInteger);

            properties.Refresh(nameof(Info), ref Info);
            properties.Refresh(nameof(SampleText), ref SampleText);
        }
    }
}