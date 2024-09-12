using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace CitrioN.Common.Editor
{
  public static class AssetUtilities
  {
    public static IEnumerable<T> GetAllAssetsOfType<T>(params string[] folders)
    {
      if (!typeof(UnityEngine.Object).IsAssignableFrom(typeof(T)))
      {
        return Enumerable.Empty<T>();
      }

      var assets = AssetDatabase.FindAssets($"t:{typeof(T).Name}", folders)
          .Select(AssetDatabase.GUIDToAssetPath)
          .Select(AssetDatabase.LoadMainAssetAtPath)
          .OfType<T>();

      EditorUtility.UnloadUnusedAssetsImmediate();
      return assets;
    }

    public static IEnumerable<UnityEngine.Object> GetAllAssetsOfName(string name)
    {
      var assets = AssetDatabase.FindAssets($"{name}")
          .Select(AssetDatabase.GUIDToAssetPath)
          .Select(AssetDatabase.LoadMainAssetAtPath);
      return assets;
    }

    public static string GetRelativePath(string absolutePath)
    {
      return absolutePath.Substring(absolutePath.LastIndexOf("Assets"));
    }
  }
}