using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CitrioN.SettingsMenuCreator
{
  [AddComponentMenu("CitrioN/Settings Menu Creator/Navigation/Navigation Setter (UGUI) - Basic Loop")]
  public class SettingsMenu_UGUI_NavigationSetter_Basic_Loop : SettingsMenu_UGUI_NavigationSetter
  {
    protected override Selectable GetNextInputElement(SettingsMenu_UGUI menu, Selectable origin,
      List<Selectable> inputElements, Dictionary<Selectable, RectTransform> selectableRoots, bool next = true)
    {
      if (origin == null) { return null; }
      var index = inputElements.IndexOf(origin);
      if (index < 0) { return null; }
      Selectable nextElement = null;
      var increment = next ? 1 : -1;
      var elementsCount = inputElements.Count;
      var maxAttempts = inputElements.Count;
      var attempts = 0;
      string settingParent = GetSettingsParent(origin, selectableRoots, menu);
      if (string.IsNullOrEmpty(settingParent)) { return null; }
      bool foundNextElement = false;

      do
      {
        attempts++;
        index = index + increment;
        // TODO Refactor this to use modulo for shorter code?
        if (index < 0)
        {
          do
          {
            index = elementsCount + index;
          } while (index < 0);
        }
        else if (index >= elementsCount)
        {
          do
          {
            var remainingCount = index - elementsCount;
            index = remainingCount;
          } while (index >= elementsCount);
        }

        nextElement = inputElements[index];

        foundNextElement = nextElement != null && nextElement.gameObject.activeInHierarchy &&
                           nextElement.IsInteractable();// &&
                                                        //settingParent == GetSettingsParent(nextElement, selectableRoots, menu);
      } while (attempts <= maxAttempts && !foundNextElement);

      return foundNextElement ? nextElement : null;
    }
  }
}