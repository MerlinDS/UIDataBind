namespace UIDataBind.Entitas.Features.Presentation
{
    public sealed class PropertiesFeature : Feature
    {
        public PropertiesFeature(UiBindContext context)
        {
            Add(new InitValueBindersSystem(context));
            Add(new BooleanValueUpdateSystem(context));
            Add(new IntValueUpdateSystem(context));
            Add(new FloatValueUpdateSystem(context));
            Add(new StringValueUpdateSystem(context));
            Add(new SpriteValueBinderSystem(context));
        }
    }
}