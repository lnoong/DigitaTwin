using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CitrioN.SettingsMenuCreator.Editor
{
  [CustomEditor(typeof(MenuWithInputElementsPrefabCreator))]
  public class MenuWithInputElementsPrefabCreatorEditor : UnityEditor.Editor
  {
    public override VisualElement CreateInspectorGUI()
    {
      var root = new VisualElement();

      // Add the default inspector
      InspectorElement.FillDefaultInspector(root, serializedObject, this);

      // Add some spacing
      var spacer = new VisualElement();
      spacer.style.height = 10;
      root.Add(spacer);

      // Create and add a button for creating/updating the prefab
      var createResourcesButton = new Button(CreatePrefab);
      createResourcesButton.text = "Create/Update Prefab";
      createResourcesButton.style.height = 30;
      root.Add(createResourcesButton);

      return root;
    }

    private void CreatePrefab()
    {
      var targetObject = serializedObject.targetObject;
      if (targetObject != null && targetObject is MenuWithInputElementsPrefabCreator creator)
      {
        creator.CreatePrefab();
      }
    }
  }
}