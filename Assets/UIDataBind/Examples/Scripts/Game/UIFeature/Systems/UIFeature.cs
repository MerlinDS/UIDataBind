using UIDataBind.Base;

namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class UIFeature : Feature
    {
        public UIFeature(Contexts contexts)
        {
            BindingPath path = "SampleView";
            Add(new InitializeSampleViewSystem(contexts.uiBind, path));
            Add(new SampleViewClickedSystem(contexts.uiBind, path));
            Add(new SampleViewHoveredSystem(contexts.uiBind, path));
            Add(new SampleViewToggledSystem(contexts.uiBind, path));
        }
    }
}