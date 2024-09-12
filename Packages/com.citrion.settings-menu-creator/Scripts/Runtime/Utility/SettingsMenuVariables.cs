using CitrioN.Common;
using System.Collections.Generic;

namespace CitrioN.SettingsMenuCreator
{
  public static class SettingsMenuVariables
  {
    public static List<string> VariableNames = new List<string>();

    public const string MIN_RANGE = "MIN_RANGE";
    public const string MAX_RANGE = "MAX_RANGE";
    public const string STEP_SIZE = "STEP_SIZE";
    public const string STEP_COUNT = "STEP_COUNT";

    public const string SETTING_VALUE_CHANGED_EVENT_NAME = "OnSettingValueChanged";

    static SettingsMenuVariables() {
      ConsoleLogger.Log("Reset variables", LogType.Debug);
      VariableNames = new List<string>();
      VariableNames.Add(MIN_RANGE);
      VariableNames.Add(MAX_RANGE);
      VariableNames.Add(STEP_SIZE);
      VariableNames.Add(STEP_COUNT);
    }
  }
}