using CitrioN.Common;
using UnityEngine;

namespace CitrioN.UI
{
  public class CloseAllUIPanels : MonoBehaviour
  {
    [SkipObfuscationRename]
    public void CloseAllPanels()
    {
      UIPanelManager.CloseAllPanels();
    }
  }
}