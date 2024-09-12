using CitrioN.Common;
using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.StyleProfileSystem
{
  public abstract class StyleListener : MonoBehaviour
  {
    [Header("General")]

    [SerializeField]
    [Tooltip("The style profile key to listen to")]
    protected string key;

    [Space(10)]

    [SerializeField]
    [Tooltip("The StyleProfile to listen for changes.\n\n" +
             "This is useful if you want to override the profile for " +
             "a specific element. In most cases you can leave this empty " +
             "and use the identifier instead because that allows you to " +
             "have multiple profiles using the same identifier allowing " +
             "you to more easily switch between profiles.\n\n" +
             "If the profile's identifier doesn't match, changes of that " +
             "style profile will be ignored.")]
    protected StyleProfile styleProfile;

    [SerializeField]
    [HideInInspector]
    protected StyleProfile lastStyleProfile;

    // TODO Add support to specify a profile name so
    // only when the correct profile is changed it will affect the listener
    // This will allow multiple profiles to affect different menus/elements
    // Then add a recursive profile name modified to the root of a menu
    // to essentially set the profile name for that menu/hierarchy.
    // Resubscribe to events when the name is changed.
    //[SerializeField]
    [Tooltip("The style profile identifier to listen for changes.\n" +
             "If this identifier doesn't match, changes of that\n" +
             "style profile will be ignored.")]
    public string styleProfileIdentifier;

    [SerializeField]
    [HideInInspector]
    protected string lastProfileIdentifier;

    //[SerializeField]
    //[Tooltip("Optional\n\nA string that will be removed from this key" +
    //         "before processing the value assigned to the key")]
    //protected string removeFromKey;

    public string StyleProfileIdentifier
    {
      get
      {
        if (styleProfile != null)
        {
          return styleProfile.Identifier;
        }
        return styleProfileIdentifier;
      }
      set
      {
        var currentIdentifier = StyleProfileIdentifier;
        styleProfileIdentifier = value;
        var requiresUpdate = currentIdentifier != StyleProfileIdentifier;

        if (requiresUpdate) { UpdateStyleFromProfile(); }
      }
    }

    public StyleProfile StyleProfile
    {
      get => styleProfile;
      set
      {
        var currentIdentifier = StyleProfileIdentifier;
        var currentProfile = styleProfile;
        styleProfile = value;
        var requiresUpdate = currentProfile != styleProfile ||
                             currentIdentifier != StyleProfileIdentifier;

        if (requiresUpdate) { UpdateStyleFromProfile(); }
      }
    }

    public string Key
    {
      get => key;
      set
      {
        var forceUpdate = key != value;
        key = value;
        if (forceUpdate)
        {
          if (styleProfile != null)
          {
            UpdateStyleFromProfile(styleProfile);
          }
          else if (!string.IsNullOrEmpty(styleProfileIdentifier))
          {
            UpdateStyleFromProfile(styleProfileIdentifier);
          }
        }
      }
    }

    protected virtual void Awake() { }

    //public virtual string ProcessedKey
    //{
    //  get
    //  {
    //    if (string.IsNullOrEmpty(removeFromKey))
    //    {
    //      return key;
    //    }

    //    return key.Replace(removeFromKey, string.Empty);
    //  }
    //}

    protected virtual void OnValidate()
    {
      if (Application.isPlaying)
      {
        // Check if either the identifier or the profile was changed
        if (lastProfileIdentifier != styleProfileIdentifier ||
            lastStyleProfile != styleProfile)
        {
          UpdateStyleFromProfile();
        }
      }

      lastStyleProfile = styleProfile;
      lastProfileIdentifier = styleProfileIdentifier;
    }

    protected virtual void OnEnable()
    {
      if (Application.isPlaying)
      {
        RegisterEvents();
        UpdateStyleFromProfile();
        //ScheduleUtility.InvokeNextFrame(() => UpdateStyleFromProfile());
      }
    }

    protected virtual void OnDisable()
    {
      if (Application.isPlaying)
      {
        UnregisterEvents();
      }
    }

    public void RegisterEvents()
    {
      UnregisterEvents();
      GlobalEventHandler.AddEventListener<StyleProfile, string, object>(StyleProfile.STYLE_CHANGED_EVENT_NAME, OnEventInvoked);
    }

    public void UnregisterEvents()
    {
      GlobalEventHandler.RemoveEventListener<StyleProfile, string, object>(StyleProfile.STYLE_CHANGED_EVENT_NAME, OnEventInvoked);
    }

    protected virtual bool CanApply(StyleProfile styleProfile, string key, object value)
    {
      return enabled && /*this.isActiveAndEnabled &&*/ IsCorrectStyleKey(key) &&
             IsCorrectProfile(styleProfile);
    }

    protected bool IsCorrectStyleKey(string key)
    {
      if (string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(key) ||
          Key != key) { return false; }

      return true;
    }

    protected bool IsCorrectProfile(StyleProfile styleProfile)
    {
      // If a style profile is specified for this listener it
      // will only consider the provided profile as correct if
      // it is the same as the one for this listener.
      if (this.styleProfile != null)
      {
        return this.styleProfile == styleProfile;
      }

      // If the identifier for this listener is no specified
      // it will consider the provided profile as correct.
      if (string.IsNullOrEmpty(styleProfileIdentifier))
      {
        return true;
      }

      // The profile is correct if it has the same identifier as
      // the one specified for this listener.
      return this.styleProfileIdentifier == styleProfile.Identifier;
    }

    protected virtual void OnEventInvoked(StyleProfile styleProfile, string key, object value)
    {
      if (CanApply(styleProfile, key, value))
      {
        ApplyChange(value);
      }
    }

    public abstract void ApplyChange(object value);

    protected bool UpdateStyleFromProfile()
    {
      if (!UpdateStyleFromProfile(styleProfile))
      {
        return UpdateStyleFromProfile(StyleProfileIdentifier);
      }
      return true;
    }

    protected virtual bool UpdateStyleFromProfile(string styleProfileIdentifier)
    {
      if (StyleProfile.GetValueFromProfile(styleProfileIdentifier, Key, out var profile, out var value))
      {
        if (profile != null)
        {
          OnEventInvoked(profile, Key, value);
          return true;
        }
      }
      return false;
    }

    public virtual bool UpdateStyleFromProfile(StyleProfile styleProfile)
    {
      if (styleProfile != null && styleProfile.GetValue(Key, out var value))
      {
        OnEventInvoked(styleProfile, Key, value);
        return true;
      }
      return false;
    }
  }

  public abstract class GenericStyleListener<T> : StyleListener
  {
    [SerializeField]
    [Tooltip("Invoked when a corresponding style change is detected")]
    protected UnityEvent<T> onStyleChanged = new UnityEvent<T>();

    public override void ApplyChange(object value)
    {
      if (value is T actualValue)
      {
        ApplyChange(actualValue);
      }
    }

    protected virtual void ApplyChange(T value)
    {
      onStyleChanged?.Invoke(value);
    }
  }
}