using UnityEngine;

namespace CitrioN.Common
{
  [SkipObfuscation]
  public class GlobalEventInvoker_Transform_FromHierarchy : GlobalEventInvoker_Transform
  {
    [SerializeField]
    protected HierarchyObjectType objectType = HierarchyObjectType.This;

    protected override Transform Argument
    {
      get
      {
        switch (objectType)
        {
          case HierarchyObjectType.This:
            return transform;
          case HierarchyObjectType.Parent:
            return transform.parent;
          case HierarchyObjectType.Child:
            return transform.GetChild(0)?.transform;
          case HierarchyObjectType.Root:
            return transform.root;
          case HierarchyObjectType.Custom:
            return argument;
        }
        return null;
      }
    }
  }

  [SkipObfuscation]
  public class GlobalEventInvoker_String : GlobalEventInvoker<string> { }

  [SkipObfuscation]
  public class GlobalEventInvoker_Transform : GlobalEventInvoker<Transform> { }
}