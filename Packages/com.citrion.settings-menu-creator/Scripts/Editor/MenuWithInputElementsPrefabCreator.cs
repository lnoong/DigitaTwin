using CitrioN.Common;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace CitrioN.SettingsMenuCreator.Editor
{
  [CreateAssetMenu(fileName = "MenuWithInputElementsPrefabCreator_",
                 menuName = "CitrioN/Settings Menu Creator/Menu With Input Elements Prefab Creator")]
  public class MenuWithInputElementsPrefabCreator : ScriptableObject
  {
    [SerializeField]
    [Tooltip("Whether or not a new prefab variant should be created. " +
             "If false any overrides will be applied to the referenced prefab.")]
    protected bool createNewPrefabVariant = true;

    [SerializeField]
    [Tooltip("Should the added input elements be initialized? " +
             "Initialized elements will for example have their labels updated.")]
    protected bool initializeInputElements = false;

    [SerializeField]
    [Tooltip("The layout prefab to use.")]
    protected GameObject menuLayoutPrefab;

    [SerializeField]
    [Tooltip("The settings collection to add the input elements for.")]
    protected SettingsCollection settingsCollection;

    public GameObject MenuLayoutPrefab
    {
      get => menuLayoutPrefab;
      set => menuLayoutPrefab = value;
    }

    public SettingsCollection SettingsCollection
    {
      get => settingsCollection;
      set => settingsCollection = value;
    }

    public bool CreateNewPrefabVariant
    {
      get => createNewPrefabVariant;
      set => createNewPrefabVariant = value;
    }

    protected bool InitializeInputElements
    {
      get => initializeInputElements;
      set => initializeInputElements = value;
    }

    public virtual void CreatePrefab()
    {
      if (menuLayoutPrefab == null) { return; }
      if (GameObjectExtensions.IsSceneObject(menuLayoutPrefab))
      {
        ConsoleLogger.LogWarning("The referenced object is a scene object which " +
                                 "does not work with the prefab creator script!");
        return;
      }
      if (settingsCollection == null) { return; }

      var layoutVariant = CreateNewPrefabVariant ? CreatePrefabVariant(menuLayoutPrefab) : menuLayoutPrefab;

      if (layoutVariant == null) { return; }

      var instance = PrefabUtility.InstantiatePrefab(layoutVariant) as GameObject;

      if (instance == null) { return; }

      //var assetPath = AssetDatabase.GetAssetPath(layoutVariant);

      //var prefabStageInstance = PrefabUtility.LoadPrefabContents(assetPath);

      //if (prefabStageInstance == null) { return; }

      var addedGameObjects = AddElements(instance.GetComponentInChildren<RectTransform>());

      PrefabUtility.ApplyPrefabInstance(instance, InteractionMode.AutomatedAction);

      DestroyImmediate(instance);
      //PrefabUtility.ApplyAddedGameObjects(addedGameObjects.ToArray(), assetPath, InteractionMode.AutomatedAction);
    }

    protected static GameObject CreatePrefabVariant(GameObject source)
    {
      string folderPath = AssetDatabase.GetAssetPath(source);
      var newFolderPath = AssetDatabase.GenerateUniqueAssetPath(folderPath);
      //ConsoleLogger.Log(newFolderPath);
      //return null;

      GameObject instance = PrefabUtility.InstantiatePrefab(source) as GameObject;
      //var prefabVariant = PrefabUtility.SaveAsPrefabAsset(instance, $"{folderPath}/{source.name}_Variant.prefab");
      var prefabVariant = PrefabUtility.SaveAsPrefabAsset(instance, newFolderPath);
      GameObject.DestroyImmediate(instance);
      EditorUtility.SetDirty(prefabVariant);
      return prefabVariant;
    }

    protected List<GameObject> AddElements(RectTransform root)
    {
      var list = new List<GameObject>();
      if (root != null && SettingsCollection != null)
      {
        if (SettingsCollection.InputElementProviders_UGUI == null)
        {
          ConsoleLogger.LogWarning($"No {nameof(InputElementProviderCollection_UGUI)} reference assigned!");
          return list;
        }
        foreach (var s in SettingsCollection.Settings)
        {
          if (s == null) { continue; }
          var elem = s.FindElement_UGUI(root, SettingsCollection);
          if (elem == null)
          {
            elem = s.CreateElement_UGUI(root, SettingsCollection);
          }
          if (elem != null)
          {
            if (InitializeInputElements)
            {
              s.InitializeElement_UGUI(elem, SettingsCollection, initialize: false);
            }
            list.Add(elem.gameObject);
          }
          else
          {
            ConsoleLogger.LogWarning($"Unable to find or create an input element for setting: " +
                         $"{s.Setting.RuntimeName.Bold()}", Common.LogType.Always);
          }
        }
      }
      return list;
    }
  }
}