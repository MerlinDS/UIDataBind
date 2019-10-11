namespace UIDataBind.Entitas.Features.Presentation
{
    public sealed class PresentationFeature : Feature
    {
        public PresentationFeature(UiBindContext context)
        {
            Add(new BindersValueUpdateSystem(context));
        }
    }
}