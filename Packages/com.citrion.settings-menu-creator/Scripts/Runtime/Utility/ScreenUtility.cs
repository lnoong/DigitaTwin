using CitrioN.Common;
using System.Globalization;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  public static class ScreenUtility
  {
    public static void SetScreenResolution(int width, int height, bool fullscreen = true)
    {
      Screen.SetResolution(width, height, fullscreen);
#if UNITY_EDITOR && UNITY_2022_1_OR_NEWER
      var windows = (UnityEditor.EditorWindow[])Resources.FindObjectsOfTypeAll(typeof(UnityEditor.EditorWindow));
      foreach (var window in windows)
      {
        if (window != null)
        {
          var fullName = window.GetType().FullName;
          if (fullName != "UnityEditor.GameView") { continue; }
          var assembly = typeof(UnityEditor.EditorWindow).Assembly;
          var type = assembly?.GetType("UnityEditor.GameView");

          var method = type?.GetPrivateMethod("SetCustomResolution");
          //var method = type?.GetPrivateMethod("SetMainPlayModeViewSize");
          //var method = type?.GetPrivateMethod("set_targetSize");
          //var methods = type.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
          //foreach (var item in methods)
          //{
          //  var parameters = item.GetParameters();
          //  if (parameters?.Length > 0)
          //  {
          //    if (parameters[0].ParameterType == typeof(Vector2))
          //    {
          //      ConsoleLogger.Log($"{item.Name}");
          //    }
          //  }
          //}

          method?.Invoke(window, new object[] { new Vector2(width, height), string.Empty });
          //method?.Invoke(window, new object[] { new Vector2(width, height) });
          break;
        }
      }

      //UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
#endif
    }

    public static bool GetScreenResolutionFromString(string resolutionString,
      out Resolution resolution, char splitCharacter = ' ')
    {
      resolution = new Resolution();
      var splitRes = resolutionString.Split(splitCharacter);
      if (splitRes.Length >= 2)
      {
        // Parse the first and last elements
        // to width and height respectively
        if (int.TryParse(splitRes[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int width) &&
            int.TryParse(splitRes[splitRes.Length - 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int height))
        {
          resolution.width = width;
          resolution.height = height;
          return true;
        }
      }
      return false;
    }

    public static string GetResolutionAsString(Resolution resolution)
      => $"{resolution.width} x {resolution.height}";

    public static string GetCurrentResolutionAsString()
      => GetResolutionAsString(Screen.currentResolution);
  }
}