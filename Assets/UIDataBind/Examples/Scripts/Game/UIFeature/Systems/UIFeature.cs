namespace UIDataBind.Examples.Game.UIFeature.Systems
{
    public sealed class UIFeature : Feature
    {
        public UIFeature(Contexts contexts)
        {
            Add(new InitializeViewModelsSystem(contexts.uiBind));
            Add(new InitializedBaseControlsModelSystem(contexts.uiBind));
            Add(new InitializedCollectionsModelSystem(contexts.uiBind));
            Add(new InitializedInfoModelSystem(contexts.uiBind));

            Add(new MenuClickSystem(contexts.uiBind));
            Add(new BaseControlsVisibleUpdateSystem(contexts.uiBind));
            Add(new CollectionsVisibleUpdateSystem(contexts.uiBind));
            Add(new InfoModelVisibleUpdateSystem(contexts.uiBind));

            Add(new MenuTabChangedSystem(contexts.uiBind));

            Add(new BaseControlsChangeIntegerSystem(contexts.uiBind));

        }
    }
}