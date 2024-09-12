using CitrioN.Common;
using UnityEngine;

namespace CitrioN.StyleProfileSystem
{
  [ExecuteInEditMode]
  public class ApplyStyleProfileToHierarchy : MonoBehaviour
  {
    [SerializeField]
    protected StyleProfile styleProfile;

#if UNITY_EDITOR
    private StyleListener[] styleListeners = null;

    private void Awake()
    {
      ConsoleLogger.Log("Awake");
    }

    private void OnEnable()
    {
      ConsoleLogger.Log("OnEnable");
      if (!Application.isPlaying)
      {
        GlobalEventHandler.AddEventListener<StyleProfile, string, object>(StyleProfile.STYLE_CHANGED_EVENT_NAME, OnStyleChanged);
      }
    }

    public void OnStyleChanged(StyleProfile styleProfile, string key, object value)
    {
      ApplyStyleProfile();
    }

    private void OnDisable()
    {
      ConsoleLogger.Log("OnDisable");
      if (!Application.isPlaying)
      {
        GlobalEventHandler.RemoveEventListener<StyleProfile, string, object>(StyleProfile.STYLE_CHANGED_EVENT_NAME, OnStyleChanged);
      }
    }

    private void OnDestroy()
    {
      ConsoleLogger.Log("Destroy");
    }

    private void OnValidate()
    {
      if (Application.isPlaying) { return; }
      if (isActiveAndEnabled)
      {
        ApplyStyleProfile();
      }
    }

    public void FetchListeners()
    {
      styleListeners = GetComponentsInChildren<StyleListener>();
    }

    [ContextMenu("Apply Style Profile")]
    public void ApplyStyleProfile()
    {
      if (Application.isPlaying) { return; }
      if (styleProfile == null) { return; }
      if (styleListeners == null) { FetchListeners(); }

      for (int i = 0; i < styleListeners.Length; i++)
      {
        var listener = styleListeners[i];

        if (listener == null) { continue; }

        if (styleProfile.GetValue(listener.Key, out var value))
        {
          listener.ApplyChange(value);
        }
      }
    }
#endif
  }
}
