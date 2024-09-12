using UnityEngine;

namespace CitrioN.Common
{
  [CreateAssetMenu(fileName = "ScriptableObjectItemData_",
                   menuName = "CitrioN/Common/ScriptableObjectItemData")]
  public class ScriptableObjectItemData : ScriptableObject
  {
    [SerializeField]
    [Tooltip("The display name for this item")]
    private string displayName;

    [SerializeField]
    [Tooltip("A group this item belongs to (Optional)")]
    private string group;

    [TextArea(minLines: 2, maxLines: 20)]
    [SerializeField]
    [Tooltip("The description for this item")]
    private string description;

    [SerializeField]
    private int priority = 1;

    public string DisplayName { get => displayName; set => displayName = value; }
    public string Group { get => group; set => group = value; }
    public string Description { get => description; set => description = value; }
    public int Priority { get => priority; set => priority = value; }
  }
}
