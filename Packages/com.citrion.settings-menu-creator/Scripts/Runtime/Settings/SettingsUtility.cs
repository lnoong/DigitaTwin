using CitrioN.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;

namespace CitrioN.SettingsMenuCreator
{
  public static class SettingsUtility
  {
    public static bool ConvertValue<T>(object input, SettingsCollection settings,
                                    List<StringToStringRelation> options, out T value)
    {
      value = default;
      if (input == null)
      {
        return false;
      }

      var inputType = input.GetType();
      var desiredType = typeof(T);
      bool isMatchingType = inputType == desiredType;

      var isString = inputType == typeof(string);
      string stringValue = isString ? input as string : input.ToString();
      bool useStringValue = false;

      //var options = collection.GetSettingHolder(settingsIdentifier)?.Options;

      var option = options.Find(o => o.Value == stringValue);
      if (option != null)
      {
        if (option.Key != option.Value && !SettingsMenuVariables.VariableNames.Contains(option.Key))
        {
          useStringValue = true;
          // The key represents the value for internal processing.
          // The corresponding value for that key can be any value that
          // should be represented to the user allowing the customization
          // of display names/values.
          stringValue = option.Key;
          //if (isString || !isMatchingType)
          //{
          //  value = (T)((object)stringValue);
          //  return true;
          //}
        }
      }


      // TODO Is this reverse assignment below required?
      //else
      //{
      //  option = options.Find(o => o.Key == stringValue);
      //  if (option != null)
      //  {
      //    stringValue = option.Value;
      //  }
      //}

      // Check if the provided input is of the correct type
      if (isMatchingType && !useStringValue)
      {
        // Return the input boxed to the correct type
        value = (T)input;
        return true;
      }

      int index = options.IndexOf(option); ;

      if (index == -1)
      {
        if (inputType == typeof(bool))
        {
          bool boolValue = (bool)input;
          index = boolValue ? 1 : 0;
        }
      }

      if (desiredType == typeof(bool))
      {
        if (stringValue.ToLowerInvariant() == "true" ||
            stringValue.ToLowerInvariant() == "1")
        {
          value = (T)Convert.ChangeType(true, typeof(T));
          return true;
        }
        else if (stringValue.ToLowerInvariant() == "false" ||
                 stringValue.ToLowerInvariant() == "0")
        {
          value = (T)Convert.ChangeType(false, typeof(T));
          return true;
        }
        else if (option != null)
        {
          if (index == 0 || index == 1)
          {
            value = (T)Convert.ChangeType(index == 1, typeof(T));
            return true;
          }
        }
      }
      if (desiredType == typeof(float))
      {
        //if (float.TryParse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture, out var floatValue))
        if (FloatExtensions.TryParseFloat(stringValue, out var floatValue))
        {
          value = (T)Convert.ChangeType(floatValue, typeof(T));
          return true;
        }
        else if (index >= 0)
        {
          //if (float.TryParse(index.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out floatValue))
          if (FloatExtensions.TryParseFloat(index.ToString(), out floatValue))
          {
            value = (T)Convert.ChangeType(floatValue, typeof(T));
            return true;
          }
        }
      }
      else if (desiredType == typeof(int))
      {
        if (int.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intValue))
        {
          value = (T)Convert.ChangeType(intValue, typeof(T));
          return true;
        }
        else if (index >= 0)
        {
          value = (T)Convert.ChangeType(index, typeof(T));
          return true;
        }
      }
      else if (desiredType.IsEnum)
      {
#if UNITY_2021_1_OR_NEWER
        if (Enum.TryParse(typeof(T), stringValue, ignoreCase: true, out var result))
        {
          value = (T)Convert.ChangeType(result, typeof(T));
          return true;
        }
#else
        if (Enum.GetNames(typeof(T1)).Contains(stringValue))
        {
          var objValue = Enum.Parse(typeof(T1), stringValue, ignoreCase: true);
          value = (T1)Convert.ChangeType(objValue, typeof(T1));
          return true;
        }
#endif
        else if (index >= 0)
        {
          if (options.Count > index)
          {
#if UNITY_2021_1_OR_NEWER
            if (Enum.TryParse(typeof(T), options[index].Key, ignoreCase: true, out result))
            {
              value = (T)Convert.ChangeType(result, typeof(T));
              return true;
            }
#else
            if (Enum.GetNames(typeof(T1)).Contains(Options[index].Key))
            {
              var objValue = Enum.Parse(typeof(T1), Options[index].Key, ignoreCase: true);
              value = (T1)Convert.ChangeType(objValue, typeof(T1));
              return true;
            }
#endif
          }

          var enumValues = Enum.GetNames(typeof(T));
          if (enumValues.Length > index)
          {
#if UNITY_2021_1_OR_NEWER
            if (Enum.TryParse(typeof(T), enumValues[index], ignoreCase: true, out result))
            {
              value = (T)Convert.ChangeType(result, typeof(T));
              return true;
            }
#else
            if (Enum.GetNames(typeof(T1)).Contains(enumValues[index]))
            {
              var objValue = Enum.Parse(typeof(T1), enumValues[index], ignoreCase: true);
              value = (T1)Convert.ChangeType(objValue, typeof(T1));
              return true;
            }
#endif
          }
        }
      }
      else if (desiredType == typeof(string))
      {
        value = (T)Convert.ChangeType(stringValue, typeof(T));
        return true;
      }

      return false;
    }

    public static List<string> GetOptions<T>()
    {
      if (typeof(T) == typeof(bool))
      {
        return new List<string> { "True", "False" };
      }
      else if (typeof(Enum).IsAssignableFrom(typeof(T)))
      {
        return Enum.GetNames(typeof(T)).ToList();
      }
      return null;
    }

    /// <summary>
    /// Fetches the matching dropdown option for the provided value.
    /// </summary>
    public static TMP_Dropdown.OptionData GetDropdownOptionForValue(TMP_Dropdown dropdown, SettingHolder settingsHolder, object value)
    {
      // TODO Should this include conversion for boolean values aka 1 equals true etc?

      var optionValue = settingsHolder.GetOptionValueForKey(value);

      var dropdownOption = dropdown.options.Find(i => i.text == optionValue);

      // Check if a dropdown option was found
      if (dropdownOption == null)
      {
        var parameterTypes = settingsHolder.Setting.ParameterTypes;
        var parameterType = parameterTypes?[0];
        if (parameterType != null)
        {
          var type = Type.GetType(parameterType);

          // Check if the type is an enum which might result in a different value
          // to test against in order to find a matching dropdown option
          if (typeof(Enum).IsAssignableFrom(type))
          {
            if (value is int intValue)
            {
              var enumName = Enum.GetName(type, value);
              dropdownOption = dropdown.options.Find(i => i.text == enumName);
            }
          }
        }
      }

      return dropdownOption;
    }

    public static string GetOptionForValue(List<string> options, SettingHolder settingsHolder, object value, out string usedValue)
    {
      // TODO Should this include conversion for boolean values aka 1 equals true etc?

      var optionValue = settingsHolder.GetOptionValueForKey(value);
      var usedOption = options.Find(c => c == optionValue);

      usedValue = optionValue;

      // Check if a dropdown option was found
      if (string.IsNullOrEmpty(usedOption))
      {
        var parameterTypes = settingsHolder.Setting.ParameterTypes;
        var parameterType = parameterTypes?[0];
        if (parameterType != null)
        {
          var type = Type.GetType(parameterType);

          // Check if the type is an enum which might result in a different value
          // to test against in order to find a matching dropdown option
          if (typeof(Enum).IsAssignableFrom(type))
          {
            if (value is int intValue)
            {
              var enumName = Enum.GetName(type, value);
              usedOption = options.Find(i => i == enumName);
              if (!string.IsNullOrEmpty(usedOption))
              {
                usedValue = enumName;
              }
            }
          }
        }
      }

      return usedOption;
    }

    public static void AddMinMaxRangeValues(this List<StringToStringRelation> relations, string min, string max)
    {
      relations.Add(new StringToStringRelation(SettingsMenuVariables.MIN_RANGE, min));
      relations.Add(new StringToStringRelation(SettingsMenuVariables.MAX_RANGE, max));
    }
    public static void AddStepSize(this List<StringToStringRelation> relations, string stepSize)
    {
      relations.Add(new StringToStringRelation(SettingsMenuVariables.STEP_SIZE, stepSize));
    }

    public static void AddStepCount(this List<StringToStringRelation> relations, string stepCount)
    {
      relations.Add(new StringToStringRelation(SettingsMenuVariables.STEP_COUNT, stepCount));
    }
  }
}