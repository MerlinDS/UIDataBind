namespace UIDataBind.Entitas.Features.PostProcessing
{
    public sealed class PostProcessingFeature : Feature
    {
        public PostProcessingFeature(UiBindContext context)
        {
            Add(new ActionEntitiesCleanupSystem(context));
            Add(new CleanupDirtyEntitiesSystems(context));
        }
    }
}