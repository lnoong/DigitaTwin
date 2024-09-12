using System;
using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "Provider_UGUI_PreviousNextSelector_",
                   menuName = "CitrioN/Settings Menu Creator/Input Element Provider/UGUI/PreviousNextSelector",
                   order = 52)]
  public class ScriptableInputElementProvider_UGUI_FromPrefab_PreviousNextSelector : ScriptableInputElementProvider_UGUI_FromPrefab_Generic<string>
  {
    [SerializeField]
    [Tooltip("Should the options be possible to\n" +
             "cycle through continuously?")]
    protected bool allowCycle = true;

    [SerializeField]
    [Tooltip("Should the previous and next buttons\n" +
             "be representing if cycling is possible?")]
    protected bool representNoCycleOnButtons = false;

    public override Type GetInputFieldType(SettingsCollection settings)
      => ProviderUtility_UGUI_PreviousNextSelector.InputFieldType;

    public override bool UpdateInputElement(RectTransform elem, string settingIdentifier,
                                                string labelText, SettingsCollection settings,
                                                List<object> values, bool initialize)
    {
      //if (!IsCorrectInputElement(elem, settings)) { return false; }

      var success = base.UpdateInputElement(elem, settingIdentifier, labelText, settings, values, initialize);

      return success && ProviderUtility_UGUI_PreviousNextSelector.UpdateInputElement(elem, settingIdentifier, settings, values,
                                                                                     initialize, allowCycle, representNoCycleOnButtons);
    }
  }
}
