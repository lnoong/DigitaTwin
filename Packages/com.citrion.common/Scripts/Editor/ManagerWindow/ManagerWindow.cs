using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CitrioN.Common.Editor
{
  public class ManagerWindow : EditorWindow
  {
    public VisualTreeAsset windowUXML;

    public StyleSheet windowStyleSheet;

    public ScriptableVisualTreeAssetCollection tabContents;

    //public ScriptablePersistentData data;

    private const string MANAGER_TAB_MENU_CLASS = "manager__tab-menu";

    protected TabMenu tabMenu;

    [SerializeField]
    [HideInInspector]
    protected int tabIndex = 0;

    protected string tabName = string.Empty;

    public static ManagerWindow ShowWindow<T>(string title) where T : ManagerWindow
    {
      var window = GetWindow<T>();
      window.titleContent = new GUIContent(title);
      window.minSize = new Vector2(700, 800);
      window.tabIndex = 0;
      window.tabName = string.Empty;
      ScheduleUtility.InvokeDelayedByFrames(() => window.SelectTab(window.tabMenu));
      return window;
    }

    public static ManagerWindow ShowWindow<T>(string title, string tabName) where T : ManagerWindow
    {
      var window = ShowWindow<T>(title);
      window.tabName = tabName;
      return window;
    }

    public void CreateGUI()
    {
      if (windowUXML != null)
      {
        var window = windowUXML.Instantiate();
        if (window != null)
        {
          window.SetFlexGrow(1);
          rootVisualElement.Add(window);
        }
      }

      if (windowStyleSheet != null)
      {
        rootVisualElement.AddStyleSheet(windowStyleSheet);
      }

      tabMenu = rootVisualElement.Q<TabMenu>(className: MANAGER_TAB_MENU_CLASS);

      if (tabMenu != null)
      {
        foreach (var item in tabContents.assets)
        {
          var content = item.GetVisualTree();
          if (content != null)
          {
            tabMenu.AddTabElement(content, item.displayName);
          }
        }
      }

      //if (data != null)
      //{
      //  var tabIndexRelation = data.data.Find(r => r.Key == "Tab Index");
      //  if (tabIndexRelation == null)
      //  {
      //    data.data.Add(new StringToGenericDataRelation<object>("Tab Index", 0));
      //  }
      //  else
      //  {
      //    tabIndex = (int) tabIndexRelation.Value;
      //  }
      //}
    }

    private void SelectTab(TabMenu tabMenu)
    {
      if (tabMenu == null) { return; }
      if (!string.IsNullOrEmpty(tabName))
      {
        var tabLabels = tabMenu.TabsContainer.Query(className: "tab-menu__tab__label").ToList();
        for (int i = 0; i < tabLabels.Count; i++)
        {
          var label = tabLabels[i] as Label;
          if (label.text == tabName)
          {
            tabIndex = i;
            break;
          }
        }
      }

      tabMenu.SelectTab(tabIndex);
      //ScheduleUtility.InvokeNextFrame(() => tabMenu.SelectTab(tabIndex));
    }
  }
}