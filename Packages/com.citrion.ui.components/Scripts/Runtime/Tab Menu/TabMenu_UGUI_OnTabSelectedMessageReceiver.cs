using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.UI
{
  [AddComponentMenu("CitrioN/UI/TabMenu (UGUI)/Tab Messages Receiver (UGUI)")]
  public class TabMenu_UGUI_OnTabSelectedMessageReceiver : MonoBehaviour
  {
    [SerializeField]
    protected UnityEvent onTabSelected = new UnityEvent();

    [SerializeField]
    protected UnityEvent onTabDeselected = new UnityEvent();

    public virtual void OnTabSelected()
    {
      onTabSelected?.Invoke();
    }

    public virtual void OnTabDeselected()
    {
      onTabDeselected?.Invoke();
    }
  } 
}