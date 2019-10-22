using UIDataBind.Base.Extensions;
using UIDataBind.Runtime.Base.Extensions;

namespace UIDataBind.Entitas.Features.PostProcessing
{
    public sealed class PostProcessingFeature : Feature
    {
        public PostProcessingFeature(UiBindContext context)
        {
            Add(new CleanupDirtyEntitiesSystems(context));
            Add(new EventEntitiesCleanupSystem(context, context.GetEngine()));
        }
    }
}