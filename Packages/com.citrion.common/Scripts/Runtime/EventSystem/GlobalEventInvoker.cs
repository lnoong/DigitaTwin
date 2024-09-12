using UnityEngine;

namespace CitrioN.Common
{

  [SkipObfuscation]
  public class GlobalEventInvoker<T> : MonoBehaviour
  {
    [SerializeField]
    protected string eventName;

    [SerializeField]
    protected T argument;

    protected virtual T Argument => argument;

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Button]
#endif
    [ContextMenu("Invoke Event")]
    public void InvokeEvent()
    {
      if (!string.IsNullOrEmpty(eventName))
      {
        GlobalEventHandler.InvokeEvent<T>(eventName, Argument);
      }
    }
  }
}