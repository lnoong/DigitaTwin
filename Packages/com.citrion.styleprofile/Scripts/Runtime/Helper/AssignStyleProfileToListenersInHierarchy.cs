using UnityEngine;

namespace CitrioN.StyleProfileSystem
{
  public class AssignStyleProfileToListenersInHierarchy : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The style profile to assign")]
    protected StyleProfile styleProfile;

    [SerializeField]
    [Tooltip("The style profile identifier to assign")]
    protected string styleProfileIdentifier;

    [SerializeField]
    [Tooltip("Should existing profiles/identifiers on StyleListeners be overriden?")]
    protected bool overrideExisting = false;

    [SerializeField]
    [Tooltip("Should the assignment happen once this script is enabled?")]
    protected bool assignOnEnable = false;

    protected StyleProfile lastAppliedStyleProfile = null;

    protected string lastAppliedStyleProfileIdentifier = string.Empty;

    public StyleProfile StyleProfile
    {
      get => styleProfile;
      set => styleProfile = value;
    }

    protected string StyleProfileIdentifier
    {
      get => styleProfileIdentifier;
      set => styleProfileIdentifier = value;
    }

    protected virtual Transform Root => transform;

    protected virtual void OnEnable()
    {
      if (assignOnEnable && Application.isPlaying)
      {
        Assign(true);
      }
    }

    protected virtual void OnValidate()
    {
      // TODO Should this also check for assignOnEnable or some other variable?
      if (Application.isPlaying)
      {
        Assign(true);
      }
    }

    public void Assign(bool forceAssign)
    {
      if (Root == null) { return; }

      if (StyleProfile != null &&
         (forceAssign || StyleProfile != lastAppliedStyleProfile))
      {
        StyleProfile.AssignStyleProfileInHierarchy(Root, overrideExisting);
        lastAppliedStyleProfile = StyleProfile;
      }

      if (/*!string.IsNullOrEmpty(StyleProfileIdentifier) &&*/
         (forceAssign || StyleProfileIdentifier != lastAppliedStyleProfileIdentifier))
      {
        StyleProfile.AssignStyleProfileIdentifierInHierarchy(Root, overrideExisting, StyleProfileIdentifier);
        lastAppliedStyleProfileIdentifier = StyleProfileIdentifier;
      }
    }

    [ContextMenu("Force Assign & Apply Style Profile In Hierarchy")]
    public void ForceAssign()
    {
      Assign(true);
    }
  }
}