namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class UIFeature : Feature
    {
        public UIFeature(Contexts contexts)
        {
            ModelsInitialization(contexts);

            Add(new ChangeDemoViewSystem(contexts.uiBind));
        }

        private void ModelsInitialization(Contexts contexts)
        {
            Add(new InitializeViewModelsSystem(contexts.uiBind));
            Add(new InitializedBaseControlsModelSystem(contexts.uiBind));
            Add(new InitializedCollectionsModelSystem(contexts.uiBind));
            Add(new InitializedInfoModelSystem(contexts.uiBind));
        }
    }
}