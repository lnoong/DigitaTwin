using CitrioN.Common.Editor;
using UnityEditor;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator.Editor
{
  public static class PreferencesAndPresetsUpdater
  {
    // TODO Should these be moved elsewhere?
    private const string packageId = "com.citrion.settings-menu-creator";
    private const string presetsFolderPath = "Packages/com.citrion.settings-menu-creator/Presets";

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptReload()
    {
      if (EditorApplication.isCompiling || EditorApplication.isUpdating)
      {
        EditorApplication.delayCall += OnScriptReload;
        return;
      }

      bool preferencesExisted = PreferencesUtility.GetOrCreateSettings<Preferences_SettingsMenuCreator_Templates>
        (PreferencesProvider_SettingsMenuCreator_Templates.fileDirectory,
         PreferencesProvider_SettingsMenuCreator_Templates.fileName, out var preferences);

      EditorApplication.delayCall += ()
        => UpdatePreferencesAndPresets(preferences, applyPresets: !preferencesExisted);
    }

    private static void UpdatePreferencesAndPresets(Preferences_SettingsMenuCreator_Templates preferences,
                                                    bool applyPresets)
    {
      if (!PackageUtilities.GetPackageVersion(packageId, out var packageVersion))
      {
        return;
      }

      if (preferences.AppliedPresetsVersion == packageVersion)
      {
        return;
      }

      PresetUtilities.UpdatePresets(presetsFolderPath);

      if (applyPresets)
      {
        // Apply the default presets for the preferences object
        PresetUtilities.ApplyPresets(preferences);
      }

      // Update the applied version
      preferences.AppliedPresetsVersion = packageVersion;

      EditorUtility.SetDirty(preferences);
      AssetDatabase.SaveAssetIfDirty(preferences);

      if (!Application.isPlaying)
      {
        ManagerWindow_SettingsMenuCreator.ShowWindow_SettingsMenuCreator();
      }
    }
  }
}