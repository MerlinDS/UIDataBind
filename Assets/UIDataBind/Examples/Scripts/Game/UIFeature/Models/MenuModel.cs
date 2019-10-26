using UIDataBind.Base;
using UIDataBind.Binders.Attributes;
using UnityEngine;

namespace UIDataBind.Examples.Game.UIFeature.Models
{
    [ViewModel]
    public struct MenuModel: IViewModel
    {
        public static readonly BindingPath Path = BindingPath.BuildFrom(nameof(MenuModel));

        public ControlEvent BaseControls;
        public ControlEvent Collections;
        public ControlEvent Info;

        public Color BaseControlsColor;
        public Color CollectionsColor;
        public Color InfoColor;

        public string InfoLabel;

        public void Refresh(IProperties properties)
        {
            properties.Refresh(nameof(BaseControls), ref BaseControls);
            properties.Refresh(nameof(Collections), ref Collections);
            properties.Refresh(nameof(Info), ref Info);

            properties.Refresh(nameof(BaseControlsColor), ref BaseControlsColor);
            properties.Refresh(nameof(CollectionsColor), ref CollectionsColor);
            properties.Refresh(nameof(InfoColor), ref InfoColor);

            properties.Refresh(nameof(InfoLabel), ref InfoLabel);
        }
    }
}