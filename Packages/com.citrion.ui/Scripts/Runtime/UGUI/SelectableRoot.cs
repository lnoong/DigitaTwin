using CitrioN.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.UI
{
  public class SelectableRoot : MonoBehaviour
  {
    [Tooltip("Invoked when an OnSelectedEventInvoker invoked its onSelected event")]
    public UnityEvent onSelected = new UnityEvent();

    [Tooltip("Invoked when an OnSelectedEventInvoker invoked its onDeselected event")]
    public UnityEvent onDeselected = new UnityEvent();

    [SerializeReference]
    [Tooltip("All event invokers to listen to")]
    protected List<OnSelectedEventInvoker> onSelectedEventInvokers = new List<OnSelectedEventInvoker>();

    protected virtual void Start()
    {
      InitInvokers();
    }

    public void InitInvokers()
    {
      RemoveListeners();
      onSelectedEventInvokers.Clear();

      var invokers = GetComponentsInChildren<OnSelectedEventInvoker>();
      foreach (var i in invokers)
      {
        onSelectedEventInvokers.AddIfNotContains(i);
      }

      AddListeners();
    }

    protected virtual void OnEnable()
    {
      AddListeners();
    }

    private void AddListeners()
    {
      foreach (var i in onSelectedEventInvokers)
      {
        i.onSelected.AddListener(OnSelected);
        i.onDeselected.AddListener(OnDeselected);
      }
    }

    private void OnDisable()
    {
      RemoveListeners();
    }

    protected void RemoveListeners()
    {
      foreach (var i in onSelectedEventInvokers)
      {
        i.onSelected.RemoveListener(OnSelected);
        i.onDeselected.RemoveListener(OnDeselected);
      }
    }

    protected virtual void OnSelected()
    {
      onSelected?.Invoke();
    }

    protected virtual void OnDeselected()
    {
      onDeselected?.Invoke();
    }
  }
}