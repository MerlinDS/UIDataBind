using System.Text;

namespace UIDataBind.Utils.Extensions
{
    public static class PathExtension
    {
        private const char PathSeparator = '.';
        private static readonly StringBuilder Sb = new StringBuilder(100);

        public static string BuildPath(this string model, string propertyName) =>
            Sb.Clear().Append(model).Append(PathSeparator).Append(propertyName).ToString();
    }
}