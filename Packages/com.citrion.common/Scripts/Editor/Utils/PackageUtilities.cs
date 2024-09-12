using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace CitrioN.Common.Editor
{
  public static class PackageUtilities
  {
    public static void PrintPackageSampleNames(string packageId, string packageVersion = "1.0.0")
    {
      var samples = Sample.FindByPackage(packageId, packageVersion);
      foreach (var sample in samples)
      {
        ConsoleLogger.Log(sample.displayName);
      }
    }

    /// <summary>
    /// Wrapper for installing/adding a package to the project
    /// </summary>
    /// <param name="packageId">The package identifier</param>
    public static void InstallPackage(string packageId)
    {
      Client.Add(packageId);
    }

    public static bool IsPackageInstalled(string packageId, out string packageVersion)
    {
      #region Old - Not working for embedded/custom packages
      //  if (!File.Exists("Packages/manifest.json")) { return false; }

      //  string json = File.ReadAllText("Packages/manifest.json");
      //  return json.Contains(packageId); 
      #endregion

      List<PackageInfo> packageJsons = AssetDatabase.FindAssets("package")
        .Select(AssetDatabase.GUIDToAssetPath).Where(i => AssetDatabase.LoadAssetAtPath<TextAsset>(i) != null)
        .Select(PackageInfo.FindForAssetPath).ToList();

      var packageJson = packageJsons?.Find(i => i != null && i.name == packageId);

      bool isInstalled = packageJson != null;
      packageVersion = isInstalled ? packageJson.version : string.Empty;
      return isInstalled;
    }

    public static bool RemovePackage(string packageId)
    {
      if (!IsPackageInstalled(packageId, out var packageVersion)) { return false; }

      Client.Remove(packageId);
      return true;
    }

    public static bool GetPackageVersion(string packageId, out string packageVersion)
    {
      var isInstalled = IsPackageInstalled(packageId, out packageVersion);
      return isInstalled;
    }

    public static void ShowPackageInPackageManager(string packageId)
    {
      Window.Open(packageId);
    }
  }
}