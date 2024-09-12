using CitrioN.Common;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [SkipObfuscationRename]
  [AddComponentMenu("CitrioN/Settings Menu Creator/Setting Object (UGUI)")]
  public class SettingObject : MonoBehaviour
  {
    [SerializeField]
    [SkipObfuscationRename]
    [Tooltip("The identifier used for this object.\n\n" +
             "If assigned the same identifier as a setting\n" +
             "it can be used to create a connection to that\n" +
             "setting. Requires a valid input element script\n" +
             "attached as well in order to function properly.\n\n" +
             "Can be used to identify a setting parent object")]
    protected string identifier = null;

    [SerializeField]
    [Tooltip("The transform to attach elements to for this setting object. " +
             "If lefts empty elements will be attached to this transform.")]
    protected Transform contentParent;

    [SkipObfuscationRename]
    public string Identifier { get => identifier; set => identifier = value; }

    public Transform GetContentParent()
    {
      return contentParent != null ? contentParent : transform;
    }
  }
}