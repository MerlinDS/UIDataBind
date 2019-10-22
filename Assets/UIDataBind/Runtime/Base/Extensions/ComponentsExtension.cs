using UIDataBind.Base.Components;

namespace UIDataBind.Base.Extensions
{
    public static class ComponentsExtension
    {
        public static string ToHumanString(this IPathComponent component)
            => $"Path [{component.Value.ToString()}]";
    }
}