using System.Linq;
using Entitas;
using JetBrains.Annotations;
using UIDataBind.Entitas.Features.PostProcessing;
using UIDataBind.Entitas.Features.Presentation;

namespace UIDataBind.Entitas.Features
{
    [UsedImplicitly]
    public sealed class UIDataBindingSystems : Systems
    {

        public UIDataBindingSystems(IContexts contexts)
        {
            var context = (UiBindContext)contexts.allContexts.First(c => c.contextInfo.name == "UiBind");
            Add(new PresentationFeature(context));
            Add(new PostProcessingFeature(context));
        }
    }
}