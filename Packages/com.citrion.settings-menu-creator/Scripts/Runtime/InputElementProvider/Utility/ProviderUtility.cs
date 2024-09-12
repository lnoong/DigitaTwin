using CitrioN.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  public static class ProviderUtility
  {
    public static bool GetValueFromValues<T>(List<object> values, out T value)
    {
      if (values?.Count > 0 && values[0] != null)
      {
        Type valueType = values[0].GetType();
        Type targetType = typeof(T);
        var firstValue = values[0];
        if (valueType == typeof(T))
        {
          value = (T)values[0];
          return true;
        }
      }

      //ConsoleLogger.Log($"Value Type: {values[0]?.GetType().Name} - Generic Type: {typeof(T1).Name}");
      value = default(T);
      return false;
    }

    public static List<string> GetOptionsList(List<StringToStringRelation> options,
                                              List<string> optionsList)
    {
      float minValue = 0;
      float maxValue = 1;
      bool minValueFound = false;
      bool maxValueFound = false;
      float stepSize = 1f;

      if (options != null && options.Count > 0)
      {
        // MIN RANGE VALUE
        if (TryGetFloatValueFromOptions(options, SettingsMenuVariables.MIN_RANGE, out minValue))
        {
          minValueFound = true;
        }

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

        // MAX RANGE VALUE
        if (TryGetFloatValueFromOptions(options, SettingsMenuVariables.MAX_RANGE, out maxValue))
        {
          maxValueFound = true;
        }

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

        // STEP SIZE VALUE
        if (!TryGetFloatValueFromOptions(options, SettingsMenuVariables.STEP_SIZE, out stepSize) || stepSize < 0)
        {
          if (minValueFound && maxValueFound && 
              TryGetFloatValueFromOptions(options, SettingsMenuVariables.STEP_COUNT, out var stepCount))
          {
            if (stepCount > 0)
            {
              stepSize = (maxValue - minValue) / stepCount;
            }
            else
            {
              // Default step size is 1/10th
              stepSize = (maxValue - minValue) * 0.1f;
            }
          }
          else
          {
            stepSize = 1f;
          }
        }

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

        // TODO Add Step count?
      }

      if (minValueFound && maxValueFound)
      {
        // We create a new list of options because the
        // min and max values were defined
        optionsList = new List<string>();
        //var truncateCount = 1 / stepSize;
        for (float i = minValue; i <= maxValue; i += stepSize)
        {
          optionsList.Add(i.ToString());
          //ConsoleLogger.Log(i.ToString());
          //i = (i + stepSize).Truncate(2);
        }
      }
      return optionsList;
    }

    public static bool TryGetFloatValueFromOptions(List<StringToStringRelation> options,
      string variableName, out float value)
    {
      var valueData = options?.Find(o => o.Key == variableName);
      if (valueData != null)
      {
        if (FloatExtensions.TryParseFloat(valueData.Value, out value))
        {
          return true;
        }
      }
      value = 0f;
      return false;
    }

    public static bool TryGetIntValueFromOptions(List<StringToStringRelation> options,
      string variableName, out int value)
    {
      var valueData = options?.Find(o => o.Key == variableName);
      if (valueData != null)
      {
        if (int.TryParse(valueData.Value, NumberStyles.Integer,
                 CultureInfo.InvariantCulture, out value))
        {
          return true;
        }
      }
      value = 0;
      return false;
    }
  }
}
