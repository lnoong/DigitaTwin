using CitrioN.Common;
using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.SettingsMenuCreator
{
  public class OnSettingValueChangeListener<T> : MonoBehaviour
  {
    [Header("Setting")]
    [SerializeField]
    protected string settingIdentifier = string.Empty;

    [Header("Settings Collection")]
    [SerializeField]
    protected SettingsCollection settingsCollection;
    [SerializeField]
    protected string settingsCollectionIdentifier = string.Empty;

    [Space(10)]

    [SerializeField]
    protected UnityEvent<T> onSettingValueChanged = new UnityEvent<T>();

    private void Awake()
    {
      AddListener();
    }

    private void OnEnable()
    {
      AddListener();
    }

    private void OnDisable()
    {
      RemoveListener();
    }

    private void AddListener()
    {
      RemoveListener();
      GlobalEventHandler.AddEventListener<Setting, string, SettingsCollection, object>
        (SettingsMenuVariables.SETTING_VALUE_CHANGED_EVENT_NAME, OnSettingValueChanged);
    }

    private void RemoveListener()
    {
      GlobalEventHandler.RemoveEventListener<Setting, string, SettingsCollection, object>
        (SettingsMenuVariables.SETTING_VALUE_CHANGED_EVENT_NAME, OnSettingValueChanged);
    }

    private void OnSettingValueChanged(Setting setting, string settingIdentifier, SettingsCollection collection, object value)
    {
      if (string.IsNullOrEmpty(this.settingIdentifier))
      {
        ConsoleLogger.LogWarning("A setting identifier needs to be specified " +
                                 "to react to a setting value change!");
        return;
      }

      if (this.settingIdentifier == settingIdentifier && value is T actualValue)
      {
        bool isCollectionSpecified = settingsCollection != null;
        bool isCollectionIdentifierSpecified = !string.IsNullOrEmpty(settingsCollectionIdentifier);

        if ((!isCollectionSpecified && !isCollectionIdentifierSpecified) || 
           (isCollectionSpecified && settingsCollection == collection) || 
           (isCollectionIdentifierSpecified && settingsCollectionIdentifier == collection.Identifier))
        {
          onSettingValueChanged?.Invoke(actualValue);
        }
      }
    }
  }
}
