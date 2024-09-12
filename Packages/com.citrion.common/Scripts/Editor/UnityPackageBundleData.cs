using UnityEngine;

namespace CitrioN.Common.Editor
{
  [CreateAssetMenu(fileName = "UnityPackageBundleData_",
                   menuName = "CitrioN/Common/UnityPackageBundleData")]
  public class UnityPackageBundleData : ScriptableObject
  {
    [SerializeField]
    [UnityPackageObject]
    [Tooltip("The Unity package file to manage")]
    private Object packageAsset;

    [SerializeField]
    [Tooltip("The display name for this bundle")]
    private string displayName;

    [SerializeField]
    [Tooltip("A group this bundle belongs to (Optional)")]
    private string group;

    [TextArea(minLines: 2, maxLines: 20)]
    [SerializeField]
    //[Tooltip("The description for this bundle")]
    private string description;

    public Object Package { get => packageAsset; protected set => packageAsset = value; }
    public string DisplayName { get => displayName; set => displayName = value; }
    public string Group { get => group; set => group = value; }
    public string Description { get => description; set => description = value; }
  }
}