using CitrioN.Common;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using PreviousNextSelector = CitrioN.UI.UIToolkit.PreviousNextSelector;

namespace CitrioN.SettingsMenuCreator
{
  // TODO Add all classes similarly to the native dropdown field
  public static class ProviderUtility_UIT_PreviousNextSelector
  {
    public static Type InputFieldType => typeof(PreviousNextSelector);

    public static VisualElement GetInputElement(string settingIdentifier)
    {
      var prevNextSelector = new PreviousNextSelector();
      prevNextSelector.AddToClassList(settingIdentifier);
      return prevNextSelector;
    }

    public static bool UpdateInputElement(VisualElement elem, string settingIdentifier,
                                          SettingsCollection settings, string labelText,
                                          List<object> values, bool initialize,
                                          bool allowCycle, bool representNoCycleOnButtons)
    {
      if (elem == null) { return false; }

      if (!ProviderUtility_UIT.IsCorrectInputElementType<PreviousNextSelector>(elem))
      {
        return false;
      }

      //var field = elem as PreviousNextSelector;
      var field = elem.Q<PreviousNextSelector>();
      if (field == null) { return false; }
      field.label?.SetText(labelText);

      var settingsHolder = settings.GetSettingHolder(settingIdentifier);

      if (initialize)
      {
        field.AddToClassList(ProviderUtility_UIT.INPUT_ELEMENT_SELECTABLE_CLASS);

        var optionsList = settingsHolder?.DisplayOptions;

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

        field.AllowCycle = allowCycle;
        field.RepresentNoCycleOnButtons = representNoCycleOnButtons;

        field.ClearOptions();
        if (optionsList != null)
        {
          field.AddOptions(optionsList);
        }
      }

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

      // OLD
      //if (values?.Count > 0)
      //{
      //  var options = settingsHolder?.Options;
      //  var value = values[0].ToString();
      //  var relation = options.Find(o => o.Key == value);
      //  if (relation != null)
      //  {
      //    value = relation.Value;
      //  }
      //  field.SetValueWithoutNotify(value);
      //}

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
        //field.TryRegisterValueChangedCallbackForSetting(settingIdentifier, settings, initialize);
      }
      return true;
    }
  }
}
