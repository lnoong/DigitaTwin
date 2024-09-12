using CitrioN.Common;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "SettingsCollection_",
                   menuName = "CitrioN/Settings Menu Creator/Settings Collection/Default",
                   order = 2)]
  [SkipObfuscationRename]
  public partial class SettingsCollection : ScriptableObject
  {
    protected static Dictionary<SettingsCollection, bool> initializedCollections =
      new Dictionary<SettingsCollection, bool>();

    [SerializeField]
    [Tooltip("An identifier for the collection. " +
             "Can be used in listeners to only react to specific collections.")]
    protected string identifier;

    [SerializeField]
    [SkipObfuscationRename]
    protected List<SettingHolder> settings = new List<SettingHolder>();

    [SerializeField]
    [Tooltip("Reference to the SettingsSaver that will handle " +
             "saving & loading for this settings collection.")]
    protected SettingsSaver settingsSaver;

    [SerializeField]
    [Tooltip("The collection of input element providers\n" +
             "used for UI Toolkit")]
    protected InputElementProviderCollection_UIT inputTemplatesUIToolkit;

    [SerializeField]
    [Tooltip("The collection of input element providers\n" +
             "used for UGUI")]
    protected InputElementProviderCollection_UGUI inputTemplatesUGUI;

    [SerializeField]
    [Tooltip("The reference for an audio mixer")]
    protected AudioMixer audioMixer;

#if UNITY_POST_PROCESSING
    [SerializeField]
    [Tooltip("The reference for a post processing profile.\n" +
             "Used for post processing profile related settings\n" +
             "when no setting specific profile override is set.")]
    protected UnityEngine.Rendering.PostProcessing.PostProcessProfile postProcessProfile;
#endif

#if UNITY_HDRP || UNITY_URP
    [SerializeField]
    [Tooltip("The reference for a volume profile.\n" +
             "Used for some HDRP specific settings.")]
    protected UnityEngine.Rendering.VolumeProfile volumeProfile;
#endif

    public Dictionary<string, object[]> pendingSettingChanges = new Dictionary<string, object[]>();

    public Dictionary<string, object> activeSettingValues = new Dictionary<string, object>();

    public Dictionary<Setting, object> startValues = new Dictionary<Setting, object>();

    public Action<string> onSettingUpdated;

#if UNITY_EDITOR
    public const string SETTINGS_COLLECTION_CHANGED_EVENT_NAME = "SettingsCollection Changed";
#endif

    public string Identifier
    {
      get => identifier;
      set => identifier = value;
    }

    public SettingsSaver SettingsSaver
    {
      get => settingsSaver;
      set => settingsSaver = value;
    }

    public List<SettingHolder> Settings
    {
      get => settings;
      set => settings = value;
    }

    public InputElementProviderCollection_UIT InputElementProviders_UIT
    {
      get => inputTemplatesUIToolkit;
      set => inputTemplatesUIToolkit = value;
    }

    public InputElementProviderCollection_UGUI InputElementProviders_UGUI
    {
      get => inputTemplatesUGUI;
      set => inputTemplatesUGUI = value;
    }

#if UNITY_POST_PROCESSING
    public UnityEngine.Rendering.PostProcessing.PostProcessProfile PostProcessProfile
    {
      get => postProcessProfile;
      set => postProcessProfile = value;
    }
#endif

#if UNITY_HDRP || UNITY_URP
    public UnityEngine.Rendering.VolumeProfile VolumeProfile
    {
      get => volumeProfile;
      set => volumeProfile = value;
    }
#endif

    public AudioMixer AudioMixer
    {
      get => audioMixer;
      set => audioMixer = value;
    }

    protected bool debugMode = false;

    private void Reset()
    {
      ClearList();
    }

    [ContextMenu("Clear All")]
    public void ClearList()
    {
      Settings.Clear();
#if UNITY_EDITOR
      GlobalEventHandler.InvokeEvent(SETTINGS_COLLECTION_CHANGED_EVENT_NAME, this);
#endif
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    protected static void StaticInit()
    {
      initializedCollections?.Clear();
    }

    [SkipObfuscationRename]
    public void Initialize()
    {
      if (initializedCollections != null &&
          initializedCollections.TryGetValue(this, out var isInitialized))
      {
        if (isInitialized) { return; }
      }

      pendingSettingChanges.Clear();
      activeSettingValues.Clear();
      if (InputElementProviders_UGUI != null)
      {
        InputElementProviders_UGUI.RefreshDictionary();
      }
      if (InputElementProviders_UIT != null)
      {
        InputElementProviders_UIT.RefreshDictionary();
      }

      // Cache the currently active values so they can later be used to reset to them
      startValues.Clear();
      onSettingUpdated = default;
      foreach (var holder in settings)
      {
        var setting = holder.Setting;
        setting.InitializeForRuntime(this);
        //var defaultValue = setting.GetDefaultValue(this);
        //setting.ApplySettingChange(this, defaultValue);
        var startValueList = setting.GetCurrentValues(this);
        var startValue = startValueList?.Count > 0 ? startValueList[0] : null;
        startValues.AddOrUpdateDictionaryItem(setting, startValue);
      }

      if (debugMode)
      {
        ConsoleLogger.Log("Initialized settings collection");
      }

      initializedCollections.AddOrUpdateDictionaryItem(this, true);
    }

    [SkipObfuscationRename]
    public SettingHolder GetSettingHolder(string settingIdentifier)
    {
      return Settings?.Find(s => s.Identifier == settingIdentifier);
    }

    [SkipObfuscationRename]
    public void UpdateSettingField(VisualElement root, string identifier)
    {
      var s = Settings.Find(s => s.Identifier == identifier);

      if (s != null)
      {
        var inputElement = s.FindElement_UIToolkit(root, this);
        s.InitializeElement_UIToolkit(inputElement, this, initialize: false);
        //ConsoleLogger.Log($"Setting {identifier} is of type '{typeString}' with value '{value}'");
      }
    }

    [SkipObfuscationRename]
    public void UpdateSettingField(RectTransform root, string identifier)
    {
      var s = Settings.Find(s => s.Identifier == identifier);

      if (s != null)
      {
        var inputElement = s.FindElement_UGUI(root, this);
        s.InitializeElement_UGUI(inputElement, this, initialize: false);
        //ConsoleLogger.Log($"Setting {identifier} is of type '{typeString}' with value '{value}'");
      }
    }

    [SkipObfuscationRename]
    public void ApplySettingChange(string settingIdentifier, bool forceApply, bool updateInputElement, params object[] args)
    {
      var settings = Settings.FindAll(s => s.Identifier == settingIdentifier);

      for (int i = 0; i < settings.Count; i++)
      {
        var setting = settings[i];

        if (setting == null) { return; }
        if (/*setting != null && */(forceApply /*|| applyImmediatelyMode == ApplyImmediatelyMode.Always*/ ||
           (/*applyImmediatelyMode == ApplyImmediatelyMode.PerSetting &&*/ setting.ApplyImmediately == true))
          /*(forceApply || setting.ApplyImmediately == true)*/)
        {
          var newValue = setting.ApplySettingChange(this, args);

          if (Application.isPlaying)
          {
            if (setting.StoreValueInternally)
            {
              activeSettingValues.AddOrUpdateDictionaryItem(settingIdentifier, newValue);
            }

            if (updateInputElement)
            {
              onSettingUpdated?.Invoke(settingIdentifier);
            }
          }
        }
        else
        {
          pendingSettingChanges.AddOrUpdateDictionaryItem(settingIdentifier, args);
        }
      }
    }

    public void RevertPendingSettingsChanges()
    {
      foreach (var item in pendingSettingChanges)
      {
        if (string.IsNullOrEmpty(item.Key)) { continue; }
        onSettingUpdated?.Invoke(item.Key);
      }
      pendingSettingChanges.Clear();
    }

    [SkipObfuscationRename]
    //[ContextMenu("Apply Settings Changes")]
    public void ApplyPendingSettingsChanges()
    {
      // TODO Bug?
      //for (int i = 0; i < pendingSettingChanges.Count; i++)
      foreach (var item in pendingSettingChanges)
      {
        var identifier = item.Key;
        var args = item.Value;

        ApplySettingChange(identifier, forceApply: true, true, args);

        if (debugMode)
        {
          string parameters = string.Empty;
          if (args?.Length > 0)
          {
            var sb = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
              sb.AppendLine(args[i].ToString());
            }
            parameters = sb.ToString();
          }

          ConsoleLogger.Log($"Applying settings change: {identifier} - {parameters}", Common.LogType.Debug);
        }
      }

      pendingSettingChanges.Clear();
    }

    public void ApplySettingValues(Dictionary<string, object> data, bool forceApply)
    {
      if (data == null || data.Count == 0) { return; }
      foreach (var item in data)
      {
        ApplySettingChange(item.Key, forceApply, true, item.Value);
      }
    }

    [SkipObfuscationRename]
    //[ContextMenu("Print Setting Values")]
    public void PrintActiveSettingValues()
    {
      foreach (var item in activeSettingValues)
      {
        var identifier = item.Key;
        var value = item.Value;
        string typeString = value != null ? value.GetType().Name : "NULL";

        ConsoleLogger.Log($"{identifier}: Type: {typeString} - Value: {value}", Common.LogType.Debug);
      }
    }

    public void SaveSettings()
    {
      if (SettingsSaver != null)
      {
        SettingsSaver.SaveSettings(this);
      }
    }

    [ContextMenu("Reset Settings To Default")]
    public void ResetToDefaultSettings()
    {
      LoadSettings(true);
      activeSettingValues.Clear();
      SaveSettings();
    }

    public void LoadSettings(bool isDefault)
    {
      LoadSettings(isDefault, true, true);
    }

    public void LoadSettings(bool isDefault, bool apply, bool forceApply)
    {
      if (SettingsSaver != null)
      {
        if (isDefault)
        {
          if (apply)
          {
            foreach (var h in settings)
            {
              var defaultValue = h.Setting.GetDefaultValue(this);
              if (!h.Setting.SkipApplyingDefault && defaultValue != null)
              {
                ApplySettingChange(h.Identifier, true, true, defaultValue);
              }
              //else
              //{
              //  ConsoleLogger.Log($"Not applying default value for {h.Setting.RuntimeName}");
              //}
            }
          }
        }
        else
        {
          var data = SettingsSaver.LoadSettings();
          if (apply)
          {
            ApplySettingValues(data, forceApply);
          }
        }
      }
    }

    public void RestoreStartValues()
    {
      foreach (var item in startValues)
      {
        var setting = item.Key;
        var value = item.Value;

        setting?.ApplySettingChange(this, value);
      }
    }

    [ContextMenu("Delete Save")]
    public void DeleteSave()
    {
      if (SettingsSaver != null)
      {
        SettingsSaver.DeleteSave();
      }
    }
  }
}