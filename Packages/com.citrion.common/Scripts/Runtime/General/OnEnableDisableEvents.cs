using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.Common
{
  public class OnEnableDisableEvents : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The action(s) to be invoked if this script is enabled.")]
    private UnityEvent onEnable = new UnityEvent();
    [SerializeField]
    [Tooltip("The action(s) to be invoked if this script is disabled.")]
    private UnityEvent onDisable = new UnityEvent();

    public UnityEvent OnEnableEvent { get => onEnable; set => onEnable = value; }
    public UnityEvent OnDisableEvent { get => onDisable; set => onDisable = value; }

    private void OnEnable()
    {
      OnEnableEvent?.Invoke();
    }

    private void OnDisable()
    {
      OnDisableEvent?.Invoke();
    }
  }
}