using CitrioN.Common;
using CitrioN.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  public static class ProviderUtility_UGUI_PreviousNextSelector
  {
    public static Type InputFieldType => typeof(PreviousNextSelector);

    public static bool UpdateInputElement(RectTransform elem, string settingIdentifier,
                                          SettingsCollection settings, List<object> values, bool initialize,
                                          bool allowCycle, bool representNoCycleOnButtons)
    {
      if (elem == null) { return false; }
      var field = elem.GetComponentInChildren<PreviousNextSelector>(true);
      if (field == null) { return false; }

      var settingsHolder = settings.GetSettingHolder(settingIdentifier);

      if (initialize)
      {
        var optionsList = settingsHolder?.DisplayOptions;

        #region Check For Variables
        var options = settingsHolder?.Options;
        optionsList = ProviderUtility.GetOptionsList(options, optionsList);
        //float minValue = 0;
        //float maxValue = 1;
        //bool minValueFound = false;
        //bool maxValueFound = false;
        //float stepSize = 1f;

        //if (options != null && options.Count > 0)
        //{
        //  // MIN RANGE VALUE
        //  var minValueData = options.Find(o => o.Key == SettingsMenuVariables.MIN_RANGE);
        //  if (minValueData != null)
        //  {
        //    //if (float.TryParse(minValueData.Value, NumberStyles.Float,
        //    //                   CultureInfo.InvariantCulture, out minValue))
        //    if (FloatExtensions.TryParseFloat(minValueData.Value, out minValue))
        //    {
        //      minValueFound = true;
        //    }
        //  }

        //  // MAX RANGE VALUE
        //  var maxValueData = options.Find(o => o.Key == SettingsMenuVariables.MAX_RANGE);
        //  if (maxValueData != null)
        //  {
        //    //if (float.TryParse(maxValueData.Value, NumberStyles.Float,
        //    //                   CultureInfo.InvariantCulture, out maxValue))
        //    if (FloatExtensions.TryParseFloat(maxValueData.Value, out maxValue))
        //    {
        //      maxValueFound = true;
        //    }
        //  }

        //  // STEP SIZE VALUE
        //  var stepSizeData = options.Find(o => o.Key == SettingsMenuVariables.STEP_SIZE);
        //  if (stepSizeData != null)
        //  {
        //    //if (!float.TryParse(stepSizeData.Value, NumberStyles.Float,
        //    //                   CultureInfo.InvariantCulture, out stepSize))
        //    if (!FloatExtensions.TryParseFloat(stepSizeData.Value, out stepSize))
        //    {
        //      stepSize = 1f;
        //    }
        //  } 
        //}

        //if (minValueFound && maxValueFound)
        //{
        //  // We create a new list of options because the
        //  // min and max values were defined
        //  optionsList = new List<string>();
        //  for (float i = minValue; i <= maxValue; i += stepSize)
        //  {
        //    optionsList.Add(i.ToString());
        //  }
        //}
        #endregion

        field.AllowCycle = allowCycle;
        field.RepresentNoCycleOnButtons = representNoCycleOnButtons;

        field.ClearOptions();
        if (optionsList != null)
        {
          field.AddOptions(optionsList);
        }
      }

      //string currentValue = null;

      //if (values?.Count > 0)
      //{
      //  var options = settingsHolder?.Options;
      //  currentValue = values[0]?.ToString();

      //  var relation = options.Find(o => o.Key == currentValue);
      //  if (relation != null)
      //  {
      //    currentValue = relation.Value;
      //  }
      //  field.SetValueWithoutNotify(currentValue);
      //}

      // TODO Test extensively
      if (values?.Count > 0 && values[0] != null)
      {
        // TODO Remove later (old)
        //var value = settingsHolder?.GetOptionValueForKey(values[0]);
        //var dropdownOption = field.Values.Find(c => c == value);

        var option = SettingsUtility.GetOptionForValue(field.Values, settingsHolder, values[0], out string value);

        if (option != null)
        {
          field.SetValueWithoutNotify(value);
        }
      }

      if (initialize)
      {
        //if (settings != null)
        //{
        //  var currentValue = field.GetValue();
        //  if (currentValue != null)
        //  {
        //    settings.ApplySettingChange(settingIdentifier, false, false, currentValue);
        //  }
        //}

        field.onValueChanged.AddListener((value)
          => settings?.ApplySettingChange(settingIdentifier, false, false, value));
      }
      return true;
    }
  }
}
