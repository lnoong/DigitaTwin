using UnityEngine;
using UnityEngine.Events;

namespace CitrioN.UI
{
  /// <summary>
  /// Continuously checks if the mouse position is over the specified
  /// <see cref="rectTransform"/>. Invokes corresponding events
  /// when the mouse position entered or exited the specified RectTransform.
  /// </summary>
  public class PointerOverRectTransformDetector : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The RectTransform to check the mouse position to be inside of")]
    protected RectTransform rectTransform;

#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.ReadOnly]
#endif
    [SerializeField]
    [Tooltip("Is the mouse currently over the RectTransform? Read Only!")]
    private bool isOverRectTransform = false;

    [Tooltip("Event invoked when the mouse position entered\n" +
             "the bounds of the specified RectTransform")]
    public UnityEvent onOverRectTransformBegan = new UnityEvent();
    [Tooltip("Event invoked when the mouse position exited\n" +
             "the bounds of the specified RectTransform")]
    public UnityEvent onOverRectTransformEnded = new UnityEvent();

    /// <summary>
    /// Is the mouse position over the <see cref="rectTransform"/>?
    /// </summary>
    public bool IsOverRectTransform
    {
      get
      {
        return isOverRectTransform;
      }
      private set
      {
        // Check if the value will be changed
        if (value != IsOverRectTransform)
        {
          if (value == true)
          {
            onOverRectTransformBegan?.Invoke();
          }
          else
          {
            onOverRectTransformEnded?.Invoke();
          }
        }
        isOverRectTransform = value;
      }
    }

    protected virtual void Awake()
    {
      if (rectTransform == null)
      {
        rectTransform = GetComponent<RectTransform>();
      }
    }

    protected virtual void OnDisable()
    {
      IsOverRectTransform = false;
    }

    protected virtual void Update()
    {
      IsOverRectTransform = RectTransformUtility.RectangleContainsScreenPoint
                                                (rectTransform, UnityEngine.Input.mousePosition);
    }
  }
}