using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CitrioN.Common.Editor
{
  [CreateAssetMenu(fileName = "SamplesController_",
                   menuName = "CitrioN/Common/ScriptableObjects/VisualTreeAsset/Controller/Samples")]
  public class SamplesController : ScriptableVisualTreeAssetController
  {
    public string samplesGroupName;

    public VisualTreeAsset bundleItem;

    private ListView bundleList = null;
    private List<UnityPackageBundleData> bundles = new List<UnityPackageBundleData>();

    private const string BUNDLE_ITEM_DISPLAY_NAME_LABEL = "bundle-list-item__display-name-label";
    private const string BUNDLE_ITEM_DESCRIPTION_LABEL_CLASS = "bundle-list-item__description-label";
    private const string BUNDLE_ITEM_IMPORT_BUTTON_CLASS = "bundle-list-item__import-button";

    private const string REFRESH_BUTTON_CLASS = "refresh-bundles-button";

    private void RefreshBundlesList()
    {
      bundles.Clear();
      var allBundles = AssetUtilities.GetAllAssetsOfType<UnityPackageBundleData>();

      foreach (var bundle in allBundles)
      {
        if (bundle.Group == samplesGroupName || string.IsNullOrEmpty(samplesGroupName))
        {
          bundles.Add(bundle);
        }
      }
      bundleList?.RefreshItems();
    }

    private void InitList(VisualElement rootVisualElement)
    {
      bundleList = rootVisualElement.Q<ListView>(/*className: LIST_CLASS*/);
      if (bundleList == null)
      {
        bundleList = new ListView();
        //bundleList.AddToClassList(LIST_CLASS);
        rootVisualElement.Add(bundleList);
      }

#if UNITY_2021_1_OR_NEWER
      //bundleList.headerTitle = "List";
      bundleList.selectionType = SelectionType.None;
      bundleList.reorderable = false;
      bundleList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
      //bundleList.reorderMode = ListViewReorderMode.Animated;
      bundleList.fixedItemHeight = 100;
      bundleList.showAddRemoveFooter = false;
#else
      bundleList.itemHeight = 100;
#endif
      bundleList.showAlternatingRowBackgrounds = AlternatingRowBackground.ContentOnly;
      bundleList.itemsSource = bundles;
      bundleList.showBorder = true;

      bundleList.makeItem = MakeListItem;
      bundleList.bindItem = BindListItem;
    }

    private void BindListItem(VisualElement elem, int index)
    {
      if (bundles.Count <= index) { return; }

      var bundle = bundles[index];

      if (bundle == null) { return; }

      var displayNameLabel = elem.Q<Label>(className: BUNDLE_ITEM_DISPLAY_NAME_LABEL);
      if (displayNameLabel != null)
      {
        displayNameLabel.text = bundle.DisplayName;
      }

      var descriptionLabel = elem.Q<Label>(className: BUNDLE_ITEM_DESCRIPTION_LABEL_CLASS);
      if (descriptionLabel != null)
      {
        descriptionLabel.text = bundle.Description;
      }

      var importButton = elem.Q<Button>(className: BUNDLE_ITEM_IMPORT_BUTTON_CLASS);
      if (importButton != null)
      {
        importButton.clickable = new Clickable(() =>
        {
          ImportBundle(bundle);
        });
      }
    }

    private void ImportBundle(UnityPackageBundleData bundle)
    {
      if (bundle == null) { return; }

      var package = bundle.Package;

      if (package == null) { return; }

      var assetPath = AssetDatabase.GetAssetPath(package);
      ProjectUtilities.ImportUnityPackageFromFilePathWithDialog(assetPath);
    }

    private VisualElement MakeListItem()
    {
      VisualElement currentElement = null;
      if (bundleItem != null)
      {
        currentElement = bundleItem.Instantiate();
      }
      else
      {
        currentElement = new Label();
      }
      return currentElement;
    }

    public override void Setup(VisualElement root)
    {
      RefreshBundlesList();

      InitList(root);

      var refreshButton = root.Q<Button>(className: REFRESH_BUTTON_CLASS);
      if (refreshButton == null)
      {
        refreshButton = new Button();
        root.Add(refreshButton);
      }

      refreshButton.text = "Refresh";
      refreshButton.clicked += RefreshBundlesList;
    }
  }
}