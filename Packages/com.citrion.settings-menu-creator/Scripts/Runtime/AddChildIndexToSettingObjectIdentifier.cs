using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  public class AddChildIndexToSettingObjectIdentifier : SettingObjectIdentifierModifier
  {
    [SerializeField]
    [Tooltip("A prefix before the child index")]
    protected string indexPrefix = "-";

    [SerializeField]
    [Tooltip("A suffix after the child index")]
    protected string indexSuffix = string.Empty;

    public override void ApplyModifier()
    {
      if (!canApply) { return; }
      if (settingObject == null) { return; }

      var identifier = settingObject.Identifier;
      identifier = AddChildIndex(identifier);
      settingObject.Identifier = identifier;
      canApply = false;
    }

    protected string AddChildIndex(string input)
    {
      var index = transform.GetSiblingIndex();

      if (index > 0)
      {
        return $"{input}{indexPrefix}{index + 1}{indexSuffix}";
      }
      return input;
    }
  }
}