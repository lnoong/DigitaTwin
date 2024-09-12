using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  public abstract class SettingObjectIdentifierModifier : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The SettingObject to modify the identifier for.")]
    protected SettingObject settingObject;

    [Tooltip("Should the modifier be possible to apply?")]
    public bool canApply = true;

    protected virtual void Awake()
    {
      if (canApply)
      {
        ApplyModifier();
      }
    }

    public virtual void ApplyModifier()
    {
      if (!canApply) { return; }
      canApply = false;
    }
  }
}