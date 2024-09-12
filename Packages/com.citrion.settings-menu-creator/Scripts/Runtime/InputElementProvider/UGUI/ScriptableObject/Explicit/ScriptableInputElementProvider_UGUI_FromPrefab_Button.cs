using CitrioN.Common;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "Provider_UGUI_Button_",
                   menuName = "CitrioN/Settings Menu Creator/Input Element Provider/UGUI/Button",
                   order = 58)]
  public class ScriptableInputElementProvider_UGUI_FromPrefab_Button : ScriptableInputElementProvider_UGUI_FromPrefab
  {
    public override Type GetInputFieldType(SettingsCollection settings) => typeof(Button);

    public override bool UpdateInputElement(RectTransform elem, string settingIdentifier,
                                                string labelText, SettingsCollection settings,
                                                List<object> values, bool initialize)
    {
      var success = base.UpdateInputElement(elem, settingIdentifier, labelText, settings, values, initialize);

      if (initialize)
      {
        var button = elem.GetComponentInChildren<Button>(true);
        if (button == null) { return false; }
        //button.SetOnClickListener(() => settings?.ApplySettingChange(settingIdentifier, forceApply: false, false, null));
        button.AddOnClickListener(() => settings?.ApplySettingChange(settingIdentifier, forceApply: false, false, null));
        return true;
      }
      return success;
    }
  }
}
