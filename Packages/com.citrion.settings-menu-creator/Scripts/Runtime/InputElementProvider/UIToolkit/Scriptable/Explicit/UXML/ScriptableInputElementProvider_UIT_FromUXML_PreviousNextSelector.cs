using CitrioN.UI.UIToolkit;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "Provider_UIT_FromUXML_PrevNextSelector_",
                   menuName = "CitrioN/Settings Menu Creator/Input Element Provider/UI Toolkit/Previous Next Selector",
                   order = 122)]
  public class ScriptableInputElementProvider_UIT_FromUXML_PreviousNextSelector : ScriptableInputElementProvider_UIT_FromUXML_Generic<PreviousNextSelector, string>
  {
    [SerializeField]
    [Tooltip("Should the options be possible to\n" +
             "cycle through continuously?")]
    protected bool allowCycle = false;

    [SerializeField]
    [Tooltip("Should the previous and next buttons\n" +
             "be representing if cycling is possible?")]
    protected bool representNoCycleOnButtons = true;

    public override Type GetInputFieldType(SettingsCollection settings)
      => ProviderUtility_UIT_PreviousNextSelector.InputFieldType;

    public override bool UpdateInputElement(VisualElement elem, string settingIdentifier,
                                            string labelText, SettingsCollection settings,
                                            List<object> values, bool initialize)
    {
      //if (!IsCorrectInputElement(elem, settings)) { return false; }

      var success = base.UpdateInputElement(elem, settingIdentifier, labelText, settings, values, initialize);

      return success && ProviderUtility_UIT_PreviousNextSelector.UpdateInputElement(elem, settingIdentifier, settings,
                                                                                    labelText, values, initialize,
                                                                                    allowCycle, representNoCycleOnButtons);
    }
  }
}
