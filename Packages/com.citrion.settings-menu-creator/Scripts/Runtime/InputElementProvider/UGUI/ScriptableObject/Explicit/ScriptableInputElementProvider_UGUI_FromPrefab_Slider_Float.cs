using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "Provider_UGUI_Slider_Float_",
                   menuName = "CitrioN/Settings Menu Creator/Input Element Provider/UGUI/Slider Float",
                   order = 54)]
  public class ScriptableInputElementProvider_UGUI_FromPrefab_Slider_Float : ScriptableInputElementProvider_UGUI_FromPrefab_Generic<float>
  {
    [SerializeField]
    [Tooltip("The minimum value of the slider")]
    protected float minSliderValue = 0;
    [SerializeField]
    [Tooltip("The maximum value of the slider")]
    protected float maxSliderValue = 1;
    //[SerializeField]
    //protected bool wholeNumbers = false;
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
                                                                       minSliderValue, maxSliderValue, wholeNumbers: false, direction);
    }
  }
}
