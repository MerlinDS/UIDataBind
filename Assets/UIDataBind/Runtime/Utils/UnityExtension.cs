using UnityEngine;

namespace UIDataBind.Utils
{
    public static class UnityExtension
    {
        public static string GetName(this Object obj) =>
            obj == null ? string.Empty : obj.name;
    }
}