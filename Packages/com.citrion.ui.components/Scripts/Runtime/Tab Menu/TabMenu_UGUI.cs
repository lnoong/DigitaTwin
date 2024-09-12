using CitrioN.Common;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CitrioN.UI
{
  [SkipObfuscation]
  [AddComponentMenu("CitrioN/UI/Tab Menu (UGUI)")]
  public class TabMenu_UGUI : MonoBehaviour
  {
    [SerializeField]
    [Tooltip("The navigation to set. Should match the layout of the tab menu tabs.")]
    protected TabNavigationMode tabNavigation = TabNavigationMode.HorizontalTop;

    [Header("Tabs")]

    [SerializeField]
    [Tooltip("Reference to the transform to parent the tabs to")]
    protected Transform tabsParent;

    [SerializeField]
    [Tooltip("The prefab to instantiate a new tab with")]
    protected GameObject tabPrefab;

    [Header("Tabs Content")]

    [SerializeField]
    [Tooltip("The parent transform to parent all content to")]
    protected Transform contentParent;

    [SerializeField]
    [Tooltip("The names for all the tabs and their corresponding content.\n\n" +
             "If the source is a already in the scene it will only be reparented." +
             "Otherwise it will be instantiated and then parented.")]
    protected List<StringToGenericDataRelation<GameObject>> contentSources =
          new List<StringToGenericDataRelation<GameObject>>();

    protected List<GameObject> tabs = new List<GameObject>();

    protected List<GameObject> contents = new List<GameObject>();

    protected Action<int> onTabChanged;

    private int selectedTabIndex = -1;

    protected const string TAB_SELECTED_MESSAGE = "OnTabSelected";
    protected const string TAB_DESELECTED_MESSAGE = "OnTabDeselected";
    protected const string TAB_CONTENT_SELECTED_MESSAGE = "OnTabContentSelected";
    protected const string TAB_CONTENT_DESELECTED_MESSAGE = "OnTabContentDeselected";

    public int TabsCount => contentSources.Count;

    protected int SelectedTabIndex { get => selectedTabIndex; set => selectedTabIndex = value; }

    public void Init()
    {
      CheckForTabs();
    }

    private void Awake()
    {
      Init();
    }

    //private void OnValidate()
    //{
    //  tabs.Capacity = contentSources.Count;
    //}

    private void CheckForTabs()
    {
      if (tabPrefab != null)
      {
        // Destroy all existing tabs
        for (int i = 0; i < tabsParent.childCount; i++)
        {
          DestroyImmediate(tabsParent.GetChild(i).gameObject);
        }
        tabs.Clear();

        // Setup required tabs
        for (int i = 0; i < contentSources.Count; i++)
        {
          var instance = Instantiate(tabPrefab);
          if (instance == null) { continue; }
          tabs.Add(instance);
          instance.SetActive(true);
          instance.transform.SetParent(tabsParent, false);

          var textElement = instance.GetComponentInChildren<TextMeshProUGUI>(true);
          textElement?.SetText(contentSources[i].Key);


          var selectables = instance.GetComponentsInChildren<Selectable>();

          foreach (var selectable in selectables)
          {
            if (selectable != null)
            {
              var onSelectedEventInvoker = selectable.gameObject.AddOrGetComponent<OnSelectedEventInvoker>();
              onSelectedEventInvoker.onSelectedGameObject.RemoveListener(SelectTab);
              onSelectedEventInvoker.onSelectedGameObject.AddListener(SelectTab);
            }
          }

          //var button = instance.GetComponentInChildren<Button>(true);
          //if (button != null)
          //{
          //  var index = i;
          //  // TODO
          //  // Could this be refactored so the index is not required?
          //  // Potentially check only for click and then get the index
          //  // from checking the hierarchy and the buttons child index
          //  // That way it would be possible to use an actual method
          //  // and have that be unsubscribed as well
          //  button.SetOnClickListener(() => SelectTab(index));
          //}
        }
      }

      // Setup content for tabs
      contents.Clear();

      for (int i = 0; i < contentSources.Count; i++)
      {
        var source = contentSources[i].Value;
        if (source == null) { continue; }
        // Check if source is a prefab
        if (!source.IsSceneObject())
        {
          // Create a new instance if the source is a prefab
          // and immediately attach it to the content parent
          source = Instantiate(source, contentParent, false);
        }

        if (source != null)
        {
          source.transform.SetParent(contentParent, false);
          contents.Add(source);
        }
      }

      UpdateTabsNavigation();

      // We select the tab a frame late so anything
      // on the tab contents has time to initialize
      this.InvokeDelayedByFrames(() => SelectTab(0));
    }

    public void RegisterTabChangedCallback(Action<int> callback)
    {
      onTabChanged += callback;
    }

    public void UnregisterTabChangedCallback(Action<int> callback)
    {
      onTabChanged -= callback;
    }

    public void SelectTab(GameObject selectedGameObject)
    {
      var tabRoot = selectedGameObject.transform.GetRoot(searchUntil: tabsParent);

      if (tabRoot != null)
      {
        for (int i = 0; i < tabs.Count; i++)
        {
          var tab = tabs[i];
          if (tab.transform == tabRoot)
          {
            SelectTab(i);
            ConsoleLogger.Log($"Selecting tab: {i}", Common.LogType.Debug);
          }
        }
      }
    }

    public void SelectTab(int tabIndex)
    {
      var tabsCount = TabsCount;

      if (tabIndex >= 0 && tabIndex < tabsCount)
      {
        // TODO Broadcast messages so animations can play?

        for (int i = 0; i < tabs.Count; i++)
        {
          var isActiveContent = tabIndex == i;

          GameObject content = contents[i];

          //if (contentParent != null && contentParent.childCount > i)
          //{
          //  content = contentParent.GetChild(i).gameObject;
          //}


          if (content != null)
          {
            content.SetActive(isActiveContent);
            if (isActiveContent)
            {
              content.BroadcastMessage(TAB_CONTENT_SELECTED_MESSAGE, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
              content.BroadcastMessage(TAB_CONTENT_DESELECTED_MESSAGE, SendMessageOptions.DontRequireReceiver);
            }
          }
        }

        if (tabIndex != selectedTabIndex)
        {

          for (int i = 0; i < tabsCount; i++)
          {
            if (i != tabIndex)
            {
              var tab = tabs[i];
              tab?.BroadcastMessage(TAB_DESELECTED_MESSAGE, SendMessageOptions.DontRequireReceiver);
            }
          }
          //if (SelectedTabIndex >= 0)
          //{
          //  var currentlySelectedTab = tabs[SelectedTabIndex];
          //  if (currentlySelectedTab != null)
          //  {
          //    currentlySelectedTab.BroadcastMessage(TAB_DESELECTED_MESSAGE, SendMessageOptions.DontRequireReceiver);
          //  }
          //}

          if (tabIndex >= 0)
          {
            var selectedTab = tabs[tabIndex];
            if (selectedTab != null)
            {
              selectedTab.BroadcastMessage(TAB_SELECTED_MESSAGE, SendMessageOptions.DontRequireReceiver);
            }
          }

          selectedTabIndex = tabIndex;
          onTabChanged?.Invoke(tabIndex);
        }

        // TODO Send messages to the tabs too so they can react?

        this.InvokeDelayedByFrames(UpdateFirstContentSelectableNavigation);
      }
    }

    [ContextMenu("Select Next Tab")]
    public void SelectNextTab()
    {
      var index = SelectedTabIndex + 1;
      if (index >= tabs.Count)
      {
        index = 0;
      }
      var tabSelectable = tabs[index].GetComponentInChildren<Selectable>();

      if (tabSelectable != null)
      {
        tabSelectable.Select();
        SelectedTabIndex = index;
        SelectTab(SelectedTabIndex);
      }
    }

    [ContextMenu("Select Previous Tab")]
    public void SelectPreviousTab()
    {
      var index = SelectedTabIndex - 1;
      if (index < 0)
      {
        index = tabs.Count - 1;
      }
      var tabSelectable = tabs[index].GetComponentInChildren<Selectable>();

      if (tabSelectable != null)
      {
        tabSelectable.Select();
        SelectedTabIndex = index;
        SelectTab(SelectedTabIndex);
      }
    }

    private void UpdateTabsNavigation()
    {
      //return;
      for (int i = 0; i < TabsCount; i++)
      {
        var tabSelectables = tabs[i].GetComponentsInChildren<Selectable>();

        int nextTabIndex = (i + 1) % TabsCount;
        int previousTabIndex = i - 1;
        if (previousTabIndex < 0)
        {
          do
          {
            previousTabIndex = TabsCount + previousTabIndex;
          } while (previousTabIndex < 0);
        }

        foreach (var s in tabSelectables)
        {
          var nav = s.navigation;
          nav.mode = Navigation.Mode.Explicit;

          switch (tabNavigation)
          {
            case TabNavigationMode.HorizontalTop:
              nav.selectOnLeft = tabs[previousTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnRight = tabs[nextTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnUp = null;
              break;
            case TabNavigationMode.HorizontalBottom:
              nav.selectOnLeft = tabs[previousTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnRight = tabs[nextTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnDown = null;
              break;
            case TabNavigationMode.VerticalLeft:
              nav.selectOnUp = tabs[previousTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnDown = tabs[nextTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnLeft = null;
              break;
            case TabNavigationMode.VerticalRight:
              nav.selectOnUp = tabs[previousTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnDown = tabs[nextTabIndex].GetComponentInChildren<Selectable>();
              nav.selectOnRight = null;
              break;
            default:
              break;
          }

          s.navigation = nav;
        }
      }
    }

    private void UpdateFirstContentSelectableNavigation()
    {
      //return;
      var selectedIndex = SelectedTabIndex;
      var activeContent = contentParent.GetChildrenTransforms().Find(t => t.gameObject.activeInHierarchy);/*contentParent.GetChild(tabIndex);*/
      var firstContentSelectable = activeContent.GetComponentInChildren<Selectable>();
      ConsoleLogger.Log($"First selectable: {firstContentSelectable}", Common.LogType.Debug);

      for (int i = 0; i < TabsCount; i++)
      {
        var tabSelectables = tabs[i].GetComponentsInChildren<Selectable>();
        //int nextTabIndex = (tabIndex + 1) % TabsCount;
        //int previousTabIndex = tabIndex - 1;
        //if (previousTabIndex < 0)
        //{
        //  do
        //  {
        //    previousTabIndex = TabsCount + previousTabIndex;
        //  } while (previousTabIndex < 0);
        //}

        foreach (var s in tabSelectables)
        {
          var nav = s.navigation;
          nav.mode = Navigation.Mode.Explicit;

          //nav.selectOnLeft = tabs[previousTabIndex].GetComponentInChildren<Selectable>();
          //nav.selectOnRight = tabs[nextTabIndex].GetComponentInChildren<Selectable>();
          //nav.selectOnUp = null;

          // Make the tab select the first item in its content
          // when clicking the corresponding button for its direction

          switch (tabNavigation)
          {
            case TabNavigationMode.HorizontalTop:
              nav.selectOnDown = firstContentSelectable;
              break;
            case TabNavigationMode.HorizontalBottom:
              nav.selectOnUp = firstContentSelectable;
              break;
            case TabNavigationMode.VerticalLeft:
              nav.selectOnRight = firstContentSelectable;
              break;
            case TabNavigationMode.VerticalRight:
              nav.selectOnLeft = firstContentSelectable;
              break;
            default:
              break;
          }

          s.navigation = nav;

          // Make the first item in the tab content
          // select the tab when clicking up
          if (i == selectedIndex && firstContentSelectable != null)
          {
            var navigation = firstContentSelectable.navigation;
            navigation.mode = Navigation.Mode.Explicit;
            navigation.selectOnUp = s;

            firstContentSelectable.navigation = navigation;
          }
        }
      }
    }
  }
}