using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "Provider_UGUI_Slider_Integer_",
                   menuName = "CitrioN/Settings Menu Creator/Input Element Provider/UGUI/Slider Integer",
                   order = 55)]
  public class ScriptableInputElementProvider_UGUI_FromPrefab_Slider_Integer : ScriptableInputElementProvider_UGUI_FromPrefab_Generic<int>
  {
    [SerializeField]
    [Tooltip("The minimum value of the slider")]
    protected int minSliderValue = 0;
    [SerializeField]
    [Tooltip("The maximum value of the slider")]
    protected int maxSliderValue = 1;
    [SerializeField]
    [Tooltip("The direction of the slider")]
    protected Slider.Direction direction = Slider.Direction.LeftToRight;

    public override Type GetInputFieldType(SettingsCollection settings)
      => ProviderUtility_UGUI_Slider.InputFieldType;

    public override bool UpdateInputElement(RectTransform elem, string settingIdentifier,
                                                string labelText, SettingsCollection settings,
                                                List<object> values, bool initialize)
    {
      //if (!IsCorrectInputElement(elem, settings)) { return false; }

      var success = base.UpdateInputElement(elem, settingIdentifier, labelText, settings, values, initialize);

      return success && ProviderUtility_UGUI_Slider.UpdateInputElement(elem, settingIdentifier, settings, values, initialize,
                                                                       minSliderValue, maxSliderValue, wholeNumbers: true, direction);
    }
  }
}
