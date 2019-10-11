namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class UIFeature : Feature
    {
        public UIFeature(Contexts contexts)
        {
            Add(new InitializeSampleViewSystem(contexts));
        }
    }
}