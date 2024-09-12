using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.Common
{
  public class OnTransformChildrenChangedListener : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The action(s) to be invoked when this transforms children have changed.")]
    protected UnityEvent onChildrenChanged = new();

    private void OnTransformChildrenChanged()
    {
      onChildrenChanged?.Invoke();
    }
  } 
}