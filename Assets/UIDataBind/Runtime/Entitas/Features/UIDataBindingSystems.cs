using System.Linq;
using Entitas;
using JetBrains.Annotations;
using UIDataBind.Entitas.Features.PostProcessing;

namespace UIDataBind.Entitas.Features
{
    [UsedImplicitly]
    public sealed class UIDataBindingSystems : Systems
    {
        public UIDataBindingSystems(IContexts contexts)
        {
            var context = (UiBindContext)contexts.allContexts.First(c => c.contextInfo.name == "UiBind");
            Add(new PostProcessingFeature(context));
        }
    }
}