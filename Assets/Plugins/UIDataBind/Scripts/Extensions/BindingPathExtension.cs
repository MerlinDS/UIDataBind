using Plugins.UIDataBind.Base;

namespace Plugins.UIDataBind.Extensions
{
    public static class BindingPathExtension
    {
        public static string GetValidPropertyPath(this BindingPath path) =>
            $"_{path.PropertyName}Property";

        public static bool IsEmpty(this BindingPath path) =>
            string.IsNullOrEmpty(path.PropertyName);
    }
}