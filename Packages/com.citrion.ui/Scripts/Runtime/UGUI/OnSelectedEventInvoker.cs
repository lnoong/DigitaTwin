using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CitrioN.UI
{
  public class OnSelectedEventInvoker : MonoBehaviour, ISelectHandler, IDeselectHandler
  {
    [Tooltip("Invoked when the OnSelected method is called")]
    public UnityEvent onSelected = new UnityEvent();

    [Tooltip("Invoked when the OnDeselected method is called")]
    public UnityEvent onDeselected = new UnityEvent();

    [Tooltip("Invoked with this GameObject when the OnSelected method is called")]
    public UnityEvent<GameObject> onSelectedGameObject = new UnityEvent<GameObject>();

    [Tooltip("Invoked with this GameObject when the OnDeselected method is called")]
    public UnityEvent<GameObject> onDeselectedGameObject = new UnityEvent<GameObject>();

    public void OnSelected()
    {
      onSelected?.Invoke();
      onSelectedGameObject?.Invoke(gameObject);
    }

    public void OnDeselected()
    {
      onDeselected?.Invoke();
      onDeselectedGameObject?.Invoke(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
      OnSelected();
    }

    public void OnDeselect(BaseEventData eventData)
    {
      OnDeselected();
    }
  }
}