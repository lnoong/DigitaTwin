using CitrioN.Common;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  public static class ProviderUtility_UGUI_Dropdown
  {
    public static Type InputFieldType => typeof(TMP_Dropdown);

    public static bool UpdateInputElement(RectTransform elem, string settingIdentifier,
                                          SettingsCollection settings, List<object> values,
                                          bool initialize)
    {
      if (elem == null) return false;
      var field = elem.GetComponentInChildren<TMP_Dropdown>(true);
      if (field == null) { return false; }

      var settingsHolder = settings.GetSettingHolder(settingIdentifier);

      if (initialize)
      {
        var optionsList = settingsHolder?.DisplayOptions;

        // TODO Move this in a centralized class so all
        // dropdown and previous next selector (and potentially others)
        // can access it without having their own duplicates
        #region Check For Variables
        var options = settingsHolder?.Options;
        optionsList = ProviderUtility.GetOptionsList(options, optionsList);

        //float minValue = 0;
        //float maxValue = 1;
        //bool minValueFound = false;
        //bool maxValueFound = false;
        //float stepSize = 1f;

        //// MIN RANGE VALUE
        //var minValueData = options.Find(o => o.Key == SettingsMenuVariables.MIN_RANGE);
        //if (minValueData != null)
        //{
        //  //if (float.TryParse(minValueData.Value, NumberStyles.Float,
        //  //                   CultureInfo.InvariantCulture, out minValue))
        //  if (FloatExtensions.TryParseFloat(minValueData.Value, out minValue))
        //  {
        //    minValueFound = true;
        //  }
        //}

        //// MAX RANGE VALUE
        //var maxValueData = options.Find(o => o.Key == SettingsMenuVariables.MAX_RANGE);
        //if (maxValueData != null)
        //{
        //  //if (float.TryParse(maxValueData.Value, NumberStyles.Float,
        //  //                   CultureInfo.InvariantCulture, out maxValue))
        //  if (FloatExtensions.TryParseFloat(maxValueData.Value, out maxValue))
        //  {
        //    maxValueFound = true;
        //  }
        //}

        //// STEP SIZE VALUE
        //var stepSizeData = options.Find(o => o.Key == SettingsMenuVariables.STEP_SIZE);
        //if (stepSizeData != null)
        //{
        //  //if (!float.TryParse(stepSizeData.Value, NumberStyles.Float,
        //  //                   CultureInfo.InvariantCulture, out stepSize))
        //  if (!FloatExtensions.TryParseFloat(stepSizeData.Value, out stepSize))
        //  {
        //    stepSize = 1f;
        //  }
        //}

        //if (minValueFound && maxValueFound)
        //{
        //  optionsList = new List<string>();
        //  for (float i = minValue; i <= maxValue; i += stepSize)
        //  {
        //    optionsList.Add(i.ToString());
        //  }
        //}
        #endregion

        field.ClearOptions();
        if (optionsList != null)
        {
          field.AddOptions(optionsList);
        }
      }

      if (values?.Count > 0 && values[0] != null)
      {
        var dropdownOption = SettingsUtility.GetDropdownOptionForValue(field, settingsHolder, values[0]);
        //var value = settingsHolder.GetOptionValueForKey(values[0]);

        //var dropdownOption = field.options.Find(i => i.text == value);

        //// Check if a dropdown option was found for the value
        //if (dropdownOption == null)
        //{
        //  var parameterTypes = settingsHolder.Setting.ParameterTypes;
        //  var parameterType = parameterTypes?[0];
        //  if (parameterType != null)
        //  {
        //    var type = Type.GetType(parameterType);
        //    if (typeof(Enum).IsAssignableFrom(type))
        //    {
        //      if (values[0] is int intValue)
        //      {
        //        var enumName = Enum.GetName(type, values[0]);
        //        ConsoleLogger.Log(enumName);
        //        value = enumName.ToString();
        //        dropdownOption = field.options.Find(i => i.text == value);
        //      }
        //    }
        //  }
        //}

        var selectedIndex = dropdownOption != null && field.options != null ?
                            field.options.IndexOf(dropdownOption) : 0;
        selectedIndex = selectedIndex.ClampLowerTo0();
        field.SetValueWithoutNotify(selectedIndex);
      }
      if (initialize)
      {
        //if (settings != null)
        //{
        //  var currentIndex = field.value;
        //  if (currentIndex > 0 && currentIndex < field.options.Count)
        //  {
        //    var optionData = field.options[currentIndex];
        //    if (optionData != null)
        //    {
        //      var currentValue = optionData.text;
        //      if (currentValue != null)
        //      {
        //        settings.ApplySettingChange(settingIdentifier, false, false, currentValue);
        //      }
        //    }
        //  }
        //}

        field.onValueChanged.AddListener((selectedIndex) =>
        {
          if (selectedIndex >= 0 && field.options?.Count > selectedIndex)
          {
            var stringValue = field.options[selectedIndex].text;
            settings?.ApplySettingChange(settingIdentifier, false, false, stringValue);
          }
        });
      }
      return true;
    }
  }
}
