using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.Common
{
  public class GlobalEventListener_NoParams : MonoBehaviour
  {
    [SerializeField]
    protected string eventName;

    [SerializeField]
    protected UnityEvent action;

    private void OnEnable()
    {
      if (!string.IsNullOrEmpty(eventName))
      {
        GlobalEventHandler.AddEventListener(eventName, OnEventInvoked);
      }
    }

    private void OnDisable()
    {
      if (!string.IsNullOrEmpty(eventName))
      {
        GlobalEventHandler.RemoveEventListener(eventName, OnEventInvoked);
      }
    }

    protected virtual void OnEventInvoked()
    {
      InvokeUnityEvent();
    }

    private void InvokeUnityEvent()
    {
      action?.Invoke();
    }
  }
}