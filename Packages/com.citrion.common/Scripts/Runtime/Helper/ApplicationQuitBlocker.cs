using UnityEngine;

namespace CitrioN.Common
{
  public class ApplicationQuitBlocker : MonoBehaviour
  {
    private void OnEnable()
    {
      ApplicationQuitListener.AddApplicationQuitBlocker(this);
    }

    private void OnDisable()
    {
      ApplicationQuitListener.RemoveApplicationQuitBlocker(this);
    }
  }
}