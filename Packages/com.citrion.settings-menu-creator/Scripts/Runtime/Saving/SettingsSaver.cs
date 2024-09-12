using CitrioN.Common;
using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [SkipObfuscationRename]
  public abstract class SettingsSaver : ScriptableObject
  {
    [SerializeField]
    [Tooltip("Should the data be added to existing data\nor wipe all data before saving?")]
    protected bool appendData = true;

    public bool AppendData { get => appendData; set => appendData = value; }

    [SkipObfuscationRename]
    public abstract void SaveSettings(SettingsCollection collection);

    [SkipObfuscationRename]
    public abstract Dictionary<string, object> LoadSettings();

    public abstract void DeleteSave();
  }
}