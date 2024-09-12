using CitrioN.Common;
using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.StyleProfileSystem
{
  [SkipObfuscationRename]
  [CreateAssetMenu(fileName = "StyleProfile_",
                   menuName = "CitrioN/Style Profile/New Style Profile",
                   order = 11)]
  public class StyleProfile : ScriptableObject
  {
    public const string STYLE_CHANGED_EVENT_NAME = "OnStyleChanged";

    [SerializeField]
    [Tooltip("An identifier checked against by listeners to determine if the change should be applied.")]
    protected string identifier;

    // Internal cache for the identifier to check for changes
    [SerializeField]
    [HideInInspector]
    private string lastIdentifier;

    [SerializeField]
    protected List<StringToGenericDataRelation<StyleProfileData>> data =
      new List<StringToGenericDataRelation<StyleProfileData>>();

    protected static Dictionary<string, List<StyleProfile>> activeProfiles =
      new Dictionary<string, List<StyleProfile>>();

    protected static Dictionary<StyleProfile, string> profileIdentifiers =
      new Dictionary<StyleProfile, string>();

    protected bool CanApply => Application.isPlaying;

    public string Identifier { get => identifier; protected set => identifier = value; }

    public List<StringToGenericDataRelation<StyleProfileData>> Data { get => data; set => data = value; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void StaticInit()
    {
      activeProfiles = new Dictionary<string, List<StyleProfile>>();
      profileIdentifiers = new Dictionary<StyleProfile, string>();
    }

    private void OnValidate()
    {
      if (!Application.isPlaying)
      {
        ForceApplyProfile();
        return;
      }

      //foreach (var item in colors)
      //{
      //  GlobalEventHandler.InvokeEvent("OnStyleChanged", item.Key, item.Value);
      //}

      if (identifier != lastIdentifier)
      {
        // Get the entry for the last identifier
        if (activeProfiles.TryGetValue(lastIdentifier, out var profiles))
        {
          profiles.Remove(this);
        }
      }
      lastIdentifier = identifier;
      ApplyProfile();
    }

    public void TryAddProfile()
    {
      if (!Application.isPlaying) { return; }
      if (string.IsNullOrEmpty(identifier))
      {
        ConsoleLogger.LogWarning("Unable to add profile because it has no identifier specified", Common.LogType.Debug);
      }

      if (activeProfiles.TryGetValue(identifier, out var profiles))
      {
        profiles.AddIfNotContains(this);
      }
      else
      {
        activeProfiles.Add(identifier, new List<StyleProfile>() { this });
      }

      profileIdentifiers.AddOrUpdateDictionaryItem(this, Identifier);

      ConsoleLogger.Log($"Added style profile '{identifier}' to active profiles.",
                  Common.LogType.Debug);
    }

    [SkipObfuscationRename]
    [ContextMenu("Apply Profile")]
    public void ApplyProfile()
    {
      ApplyValues();
    }

    public void ForceApplyProfile()
    {
      foreach (var item in data)
      {
        GlobalEventHandler.InvokeEvent(STYLE_CHANGED_EVENT_NAME, this, item.Key, item.Value.GetValue());
      }
    }

    [SkipObfuscationRename]
    public void ApplyValues()
    {
      TryAddProfile();
      if (!CanApply) return;

      foreach (var item in data)
      {
        GlobalEventHandler.InvokeEvent(STYLE_CHANGED_EVENT_NAME, this, item.Key, item.Value.GetValue());
      }
    }

    public bool GetValue(string key, out object value)
    {
      TryAddProfile();

      var item = data.Find(d => d.Key == key);
      if (item != null)
      {
        value = item.Value.GetValue();
        return true;
      }

      value = null;
      return false;
    }

    public static bool GetValueFromProfile(string profileIdentifier, string key, out StyleProfile profile, out object value)
    {
      if (activeProfiles.TryGetValue(profileIdentifier, out var profiles))
      {
        if (profiles.Count > 0)
        {
          // Iterate over all profiles for the specified identifier
          // and try and find a valid profile
          for (int i = 0; i < profiles.Count; i++)
          {
            profile = profiles[i];
            if (profile != null)
            {
              return profile.GetValue(key, out value);
            }
          }
        }
      }
      value = null;
      profile = null;
      return false;
    }

    public void AssignStyleProfileInHierarchy(Transform root, bool overrideExisting)
    {
      if (root == null) { return; }

      var listeners = root.GetComponentsInChildren<StyleListener>(true, true);
      if (listeners == null) { return; }

      foreach (var l in listeners)
      {
        bool assign = overrideExisting || l.StyleProfile == null;

        if (assign)
        {
          l.StyleProfile = this;
        }
      }
    }

    public static void AssignStyleProfileIdentifierInHierarchy(Transform root, bool overrideExisting, string identifier)
    {
      if (root == null) { return; }

      var listeners = root.GetComponentsInChildren<StyleListener>(true, true);
      if (listeners == null) { return; }

      foreach (var l in listeners)
      {
        bool assign = overrideExisting || string.IsNullOrEmpty(l.styleProfileIdentifier);

        if (assign)
        {
          l.StyleProfileIdentifier = identifier;
        }
      }
    }

    public void AssignStyleProfileIdentifierInHierarchy(Transform root, bool overrideExisting)
    {
      AssignStyleProfileIdentifierInHierarchy(root, overrideExisting, Identifier);
    }
  }
}