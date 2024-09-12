using UnityEngine;

namespace CitrioN.Common
{
  public class GlobalEventInvoker_NoParams : MonoBehaviour
  {
    [SerializeField]
    protected string eventName;

    [Button]
    [SkipObfuscationRename]
    public void InvokeEvent()
    {
      if (!string.IsNullOrEmpty(eventName))
      {
        GlobalEventHandler.InvokeEvent(eventName);
      }
    }
  }
}