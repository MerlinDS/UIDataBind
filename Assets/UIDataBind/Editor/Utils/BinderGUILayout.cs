using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UIDataBind.Editor.Utils
{
    public static class BinderGUILayout
    {
        private const string EmptyOption = "None";

        public static string Popup(string selected, IEnumerable<string> displayedOptions)
        {
            var s = selected;
            var options = displayedOptions.ToList();
            if (string.IsNullOrEmpty(selected))
                s = EmptyOption;

            options.Insert(0, EmptyOption);
            var selectedIndex = options.FindIndex(x => x == s);
            if (selectedIndex < 0)
                selectedIndex = 0;

            EditorGUI.BeginChangeCheck();
            var modelIndex = EditorGUILayout.Popup(selectedIndex, options.ToArray());
            if (!EditorGUI.EndChangeCheck() || modelIndex < 0 || modelIndex >= options.Count)
                return selected;

            selected = options[modelIndex];
            return selected == EmptyOption ? string.Empty : selected;
        }
    }
}