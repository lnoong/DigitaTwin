using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.UI
{
  [AddComponentMenu("CitrioN/UI/TabMenu (UGUI)/Tab Content Messages Receiver (UGUI)")]
  public class OnTabContentSelectedReceiver : MonoBehaviour
  {
    [SerializeField]
    protected UnityEvent onTabContentSelected = new UnityEvent();

    [SerializeField]
    protected UnityEvent onTabContentDeselected = new UnityEvent();

    public virtual void OnTabContentSelected()
    {
      onTabContentSelected?.Invoke();
    }

    public virtual void OnTabContentDeselected()
    {
      onTabContentDeselected?.Invoke();
    }
  }
}