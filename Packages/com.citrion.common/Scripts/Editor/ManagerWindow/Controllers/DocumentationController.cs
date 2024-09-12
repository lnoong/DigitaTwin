using UnityEngine;
using UnityEngine.UIElements;

namespace CitrioN.Common.Editor
{
  [CreateAssetMenu(fileName = "DocumentationController_",
                   menuName = "CitrioN/Common/ScriptableObjects/VisualTreeAsset/Controller/Documentation")]
  public class DocumentationController : ScriptableVisualTreeAssetController
  {
    public string groupName;

    public VisualTreeAsset listItemTemplate;

    public override void Setup(VisualElement root)
    {
      if (!string.IsNullOrEmpty(groupName) && listItemTemplate != null)
      {
        new DocumentationDataList(groupName, listItemTemplate, root);
      }
    }
  }
}