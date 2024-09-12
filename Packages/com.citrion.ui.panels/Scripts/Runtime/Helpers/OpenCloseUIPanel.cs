using UnityEngine;

namespace CitrioN.UI
{
  /// <summary>
  /// Allows the opening or closing of a specified <see cref="AbstractUIPanel"/>.
  /// </summary>
  public class OpenCloseUIPanel : MonoBehaviour
  {
    [SerializeField]
    protected AbstractUIPanel panel;

    public void OpenPanel()
    {
      if (panel != null) { panel.OpenNoParams(); }
    }

    public void ClosePanel()
    {
      if (panel != null) { panel.CloseNoParams(); }
    }
  }
}