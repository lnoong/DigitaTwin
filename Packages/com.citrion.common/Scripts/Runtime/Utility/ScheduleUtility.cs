using System;
using UnityEditor;

namespace CitrioN.Common
{
  public static class ScheduleUtility
  {
    public static void InvokeDelayedByFrames(Action action, int frames = 1)
    {
#if UNITY_EDITOR
      if (EditorApplication.isPlaying)
      {
        if (CoroutineRunner.Instance != null)
        {
          CoroutineRunner.Instance.InvokeDelayedByFrames(action, frames);
        }
      }
      else
      {
        // TODO
        // Should this also be invoked delayed by the actual frame count?
        EditorApplication.delayCall += action.Invoke;
      }
#else
      if (CoroutineRunner.Instance != null)
      {
        CoroutineRunner.Instance.InvokeDelayedByFrames(action, frames);
      }
#endif
    }

  }
}