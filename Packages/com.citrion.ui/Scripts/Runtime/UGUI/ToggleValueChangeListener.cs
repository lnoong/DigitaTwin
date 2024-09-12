using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CitrioN.UI
{
  /// <summary>
  /// Listens to <see cref="Toggle.onValueChanged"/> and invokes
  /// events based on the new value of <see cref="Toggle.isOn"/>
  /// </summary>
  [RequireComponent(typeof(Toggle))]
  public class ToggleValueChangeListener : MonoBehaviour
  {
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ReadOnly]
#endif
    [SerializeField]
    protected Toggle toggle;

    public UnityEvent onBecameOn = new UnityEvent();
    public UnityEvent onBecameOff = new UnityEvent();

    public event Action onBecameOnAction;
    public event Action onBecameOffAction;

    private void Reset()
    {
      toggle = GetComponent<Toggle>();
    }

    private void Awake()
    {
      if (toggle == null)
      {
        toggle = GetComponent<Toggle>();
      }
    }

    private void OnEnable()
    {
      toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
      toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool isOn)
    {
      if (isOn)
      {
        onBecameOn?.Invoke();
        onBecameOnAction?.Invoke();
      }
      else
      {
        onBecameOff?.Invoke();
        onBecameOffAction?.Invoke();
      }
    }
  }
}