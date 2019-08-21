using System.IO;
using System.Linq;
using UnityEditor;

namespace Plugins.UIDataBind.Editor
{
    public static class UIDataBindPackageUtilities
    {
        /// <summary>
        ///
        /// </summary>
        [MenuItem("Window/UIDataBind/Import UIDataBind Examples", false, 2050)]
        public static void ImportExamplesContentMenu()
        {
            var packageFullPath = GetPackageFullPath();
            AssetDatabase.ImportPackage(packageFullPath + "/Package Resources/Examples.unitypackage", true);
        }

        private static string GetPackageFullPath()
        {
            // Check for potential UPM package
            var packagePath = Path.GetFullPath(@"Packages/me.merlinds.uidatabind");
            if (Directory.Exists(packagePath))
                return packagePath;

            packagePath = Path.GetFullPath("Assets/..");
            packagePath = Directory.GetDirectories(packagePath, "UIDataBind*", SearchOption.TopDirectoryOnly)
                .FirstOrDefault(p=>Directory.Exists(p + "/Editor Resources"));

            return packagePath;
        }
    }
}