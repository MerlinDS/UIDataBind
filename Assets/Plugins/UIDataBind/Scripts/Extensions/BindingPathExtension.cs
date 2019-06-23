using Plugins.UIDataBind.Base;

namespace Plugins.UIDataBind.Extensions
{
    public static class BindingPathExtension
    {
        public static bool IsEmpty(this BindingPath path) =>
            string.IsNullOrEmpty(path.PropertyName);
    }
}