using CitrioN.Common;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator
{
  public static class ProviderUtility_UIT_Label
  {
    public static Type InputFieldType => typeof(Label);

    public static VisualElement GetInputElement(string settingIdentifier)
    {
      var label = new Label();
      label.AddToClassList(settingIdentifier);
      return label;
    }

    public static bool UpdateInputElement(VisualElement elem, string labelText)
    {
      if (elem == null) { return false; }

      if (!ProviderUtility_UIT.IsCorrectInputElementType<Label>(elem))
      {
        return false;
      }

      //var label = elem as Label;
      var label = elem.Q<Label>();
      if (label == null) { return false; }
      label.SetText(labelText);

      return true;
    }
  }
}
