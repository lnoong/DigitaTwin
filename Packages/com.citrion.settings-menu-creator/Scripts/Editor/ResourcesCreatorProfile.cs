using CitrioN.Common;
using CitrioN.StyleProfileSystem;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator.Editor
{
  [CreateAssetMenu(fileName = "ResourcesCreatorProfile_",
                   menuName = "CitrioN/Settings Menu Creator/Resources Creator Profile")]
  public class ResourcesCreatorProfile : ScriptableObject
  {
    [SerializeField]
    [Tooltip("Should the entire folder structure be created even if no files are in them?")]
    protected bool createAllFolders = false;
    [SerializeField]
    [Tooltip("Should a new scene containing the settings menu prefab (if specified) be created?")]
    protected bool createDedicatedScene = true;

    [SerializeField]
    [Tooltip("A collection to duplicate (including all its references).\n" +
             "This is useful if you already have a collection you want to use as a starting point.\n" +
             "This could for example be a demo scene collection you like to use for your own settings menu.\n\n" +
             "Note:\n" +
             "The settings menu prefab itself can not be duplicated as it is not referenced on the collection!")]
    protected SettingsCollection sourceCollection;
    [SerializeField]
    [Tooltip("A list of settings collection. All settings in those collections will be added to the generated settings collection.")]
    protected List<SettingsCollection> additionalSettings = new List<SettingsCollection>();

    [SerializeField]
    [Tooltip("The settings saver to duplicate.")]
    protected SettingsSaver settingsSaver;

    //[Header("UGUI")]
    [SerializeField]
    [Tooltip("The settings menu to create a variant of.")]
    protected SettingsMenu_UGUI settingsMenu_UGUI;
    [SerializeField]
    [Tooltip("The settings menu layout to create a variant of.")]
    protected GameObject menuLayoutTemplate;
    [SerializeField]
    [Tooltip("The style profile to duplicate.")]
    protected StyleProfile styleProfile;
    [SerializeField]
    [Tooltip("The input element providers to duplicate.")]
    InputElementProviderCollection_UGUI inputElementProviders_UGUI;

    //[Header("UI Toolkit")]
    [SerializeField]
    [Tooltip("The settings menu to create a variant of.")]
    protected SettingsMenu_UIT settingsMenu_UIT;
    [SerializeField]
    [Tooltip("The UXML files to duplicate.")]
    protected List<VisualTreeAsset> menuDocuments = new List<VisualTreeAsset>();
    [SerializeField]
    [Tooltip("The USS files to duplicate.")]
    protected List<StyleSheet> menuStyleSheets = new List<StyleSheet>();
    [SerializeField]
    [Tooltip("The style profile to duplicate.")]
    protected StyleProfile styleProfile_UIT;
    [SerializeField]
    [Tooltip("The input element providers to duplicate.")]
    InputElementProviderCollection_UIT inputElementProviders_UIT;

    [MenuItem("CONTEXT/ResourcesCreatorProfile/Create Resources", false, 1)]
    private static void Command_CreateResourcesFromProfile(MenuCommand command)
    {
      var profile = (ResourcesCreatorProfile)command.context;
      CreateResourcesFromProfile(profile);
    }

    public static void CreateResourcesFromProfile(ResourcesCreatorProfile profile)
    {
      if (profile == null) { return; }

      var collection = profile.sourceCollection != null ?
        profile.sourceCollection : CreateInstance<SettingsCollection>();

      if (profile.additionalSettings != null)
      {
        foreach (var c in profile.additionalSettings)
        {
          for (int i = 0; i < c.Settings.Count; i++)
          {
            var setting = c.Settings[i];
            var duplicate = SettingHolder.GetCopy(setting);
            if (duplicate == null)
            {
              ConsoleLogger.LogWarning("SettingHolder duplicate is null");
            }
            else
            {
              collection.Settings.Add(duplicate);
            }
          }
        }
      }

      //// Assign UGUI providers
      //if (profile.inputElementProviders_UGUI != null)
      //{
      //  collection.InputElementProviders_UGUI = profile.inputElementProviders_UGUI;
      //}

      //// Assign UI Toolkit providers
      //if (profile.inputElementProviders_UIT != null)
      //{
      //  collection.InputElementProviders_UIT = profile.inputElementProviders_UIT;
      //}

      SettingsSaver settingsSaver = profile.settingsSaver;

      if (settingsSaver == null && collection != null)
      {
        settingsSaver = collection.SettingsSaver;
      }

      #region UGUI
      StyleProfile styleProfile = profile.styleProfile;
      GameObject menuLayoutTemplate = profile.menuLayoutTemplate;
      InputElementProviderCollection_UGUI inputElementProviders_UGUI = profile.inputElementProviders_UGUI;

      if (profile.settingsMenu_UGUI != null)
      {
        var menu_UGUI = profile.settingsMenu_UGUI.GetComponent<SettingsMenu_UGUI>();

        if (menu_UGUI != null)
        {
          // Style Profile
          if (styleProfile == null)
          {
            var assigner = menu_UGUI.GetComponent<AssignStyleProfileToListenersInHierarchy>();
            if (assigner != null)
            {
              styleProfile = assigner.StyleProfile;
            }
          }

          // Menu Layout Prefab
          if (menuLayoutTemplate == null)
          {
            menuLayoutTemplate = menu_UGUI.MenuTemplate;
          }
        }

        if (collection != null)
        {
          //Input Element Providers
          if (inputElementProviders_UGUI == null)
          {
            inputElementProviders_UGUI = collection.InputElementProviders_UGUI;
          }
        }
      }
      #endregion

      #region UI Toolkit
      List<VisualTreeAsset> menuDocuments = profile.menuDocuments;
      List<StyleSheet> menuStyleSheets = profile.menuStyleSheets;
      StyleProfile styleProfile_UIT = profile.styleProfile_UIT;
      InputElementProviderCollection_UIT inputElementProviders_UIT = profile.inputElementProviders_UIT;

      if (profile.settingsMenu_UIT != null)
      {
        var menu_UIT = profile.settingsMenu_UIT.GetComponent<SettingsMenu_UIT>();

        if (menu_UIT != null)
        {
          // Menu Documents
          if (menuDocuments == null || menuDocuments.Count < 1)
          {
            menuDocuments = menu_UIT.MenuTemplates;
          }

          // Menu Style Sheets
          if (menuStyleSheets == null || menuStyleSheets.Count < 1)
          {
            menuStyleSheets = menu_UIT.StyleSheets;
          }
        }

        // Style Profile
        if (styleProfile_UIT == null)
        {
          var assigner = menu_UIT.GetComponent<AssignStyleProfileToListenersInHierarchy>();
          if (assigner != null)
          {
            styleProfile_UIT = assigner.StyleProfile;
          }
        }
      }

      if (collection != null)
      {
        //Input Element Providers
        if (inputElementProviders_UIT == null)
        {
          inputElementProviders_UIT = collection.InputElementProviders_UIT;
        }
      }
      #endregion

      // Select the profile so the resources will be generated in the same folder
      Selection.activeObject = profile;

      SettingsCollectionCreationUtility.CreateSettingsCollectionAndResources
        (collection, false, false,
        styleProfile, menuLayoutTemplate,
        profile.settingsMenu_UGUI, inputElementProviders_UGUI,
        profile.settingsMenu_UIT,
        menuDocuments, menuStyleSheets, styleProfile_UIT,
        inputElementProviders_UIT, profile.createAllFolders, profile.createDedicatedScene,
        settingsSaver);
    }
  }
}