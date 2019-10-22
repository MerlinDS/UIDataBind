using System;
using System.Text;
using UIDataBind.Base;

namespace UIDataBind.Utils.Extensions
{
    public static class BindingPathExtension
    {
        private const char PathSeparator = '.';
        private static readonly StringBuilder Sb = new StringBuilder(100);

        [Obsolete]
        public static OldBindingPath BuildPath(this OldBindingPath model, OldBindingPath path) =>
            Sb.Clear().Append(model).Append(PathSeparator).Append(path).ToString();
    }
}