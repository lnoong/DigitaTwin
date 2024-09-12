using UnityEngine.UIElements;

namespace CitrioN.Common.Editor
{
  public class EditorFromVisualTreeAsset : UnityEditor.Editor
  {
    public virtual string UxmlPath => $"Packages/com.citrion.common/UI Toolkit/UXML/Editors/{GetType().Name}.uxml";

    public virtual string StyleSheetPath => $"Packages/com.citrion.common/UI Toolkit/USS/Editors/{GetType().Name}";

    public override VisualElement CreateInspectorGUI()
    {
      return UIToolkitEditorExtensions.CreateVisualElementFromTemplate
        (UxmlPath, StyleSheetPath, $"editor__{GetType().Name}");
    }
  }
}