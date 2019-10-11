using UIDataBind.Base;
using UIDataBind.Entitas.Wrappers;

namespace UIDataBind.Entitas.Extensions
{
    public static class EntitasPropertiesExtension
    {
        public static IProperties GetProperties(this UiBindContext context, string modelPath) =>
            new EntitasContextWrapper(context, modelPath);


        public static void ReadProperty(this IProperties properties, string propertyName, ref bool value)
        {
            var entity = properties.GetPropertyEntity<UiBindEntity>(propertyName);
            if (entity?.hasBooleanProperty ?? false)
                value = entity.booleanProperty.Value;
        }

        public static void WriteProperty(this IProperties properties, string propertyName, bool value)
        {
            var entity = properties.GetPropertyEntity<UiBindEntity>(propertyName, true);
            //TODO: Check if value was changed
            if (!entity.hasBooleanProperty)
                entity.AddBooleanProperty(value);
            else
                entity.ReplaceBooleanProperty(value);
        }
    }
}