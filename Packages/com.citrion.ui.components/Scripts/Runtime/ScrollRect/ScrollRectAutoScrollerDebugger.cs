using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.UI
{
  public class ScrollRectAutoScrollerDebugger : MonoBehaviour
  {
    [SerializeField]
    protected List<ScrollRectAutoScroller> activeAutoScrollers = new List<ScrollRectAutoScroller>();

#if UNITY_EDITOR
    void Update()
    {
      this.activeAutoScrollers = ScrollRectAutoScroller.activeAutoScrollers;
    } 
#endif
  } 
}