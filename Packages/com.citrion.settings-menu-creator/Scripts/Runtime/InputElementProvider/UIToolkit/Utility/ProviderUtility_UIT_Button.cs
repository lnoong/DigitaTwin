using CitrioN.Common;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator
{
  public static class ProviderUtility_UIT_Button
  {
    public static Type InputFieldType => typeof(Button);

    public static VisualElement GetInputElement(string settingIdentifier)
    {
      var button = new Button();
      button.AddToClassList(settingIdentifier);
      return button;
    }

    public static bool UpdateInputElement(VisualElement elem, string settingIdentifier,
                                          SettingsCollection settings, string labelText,
                                          List<object> values, bool initialize)
    {
      if (elem == null) { return false; }

      if (!ProviderUtility_UIT.IsCorrectInputElementType<Button>(elem))
      {
        return false;
      }

      //var button = elem as Button;
      var button = elem.Q<Button>();
      if (button == null) { return false; }
      // TODO Add localization
      button.text = labelText;

      if (initialize)
      {
        button.AddToClassList(ProviderUtility_UIT.INPUT_ELEMENT_SELECTABLE_CLASS);

        if (settings != null)
        {
          //button.clickable = new Clickable(()
          //  => settings?.ApplySettingChange(settingIdentifier, forceApply: false, false, null));
          button.clicked += () => settings.ApplySettingChange(settingIdentifier, forceApply: false, false, null);
        }
      }
      return true;
    }
  }
}
