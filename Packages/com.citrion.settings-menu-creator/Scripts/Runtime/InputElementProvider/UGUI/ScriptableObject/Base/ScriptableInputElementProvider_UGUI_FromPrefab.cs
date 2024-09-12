using CitrioN.Common;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "Provider_UGUI_FromPrefab_",
                   menuName = "CitrioN/Settings Menu Creator/Input Element Provider/UGUI/Prefab",
                   order = 69)]
  public class ScriptableInputElementProvider_UGUI_FromPrefab : ScriptableInputElementProvider_UGUI
  {
    [SerializeField]
    [Tooltip("The prefabs to instantiate as the element to provide. " +
             "If more than one prefab is provided a ProviderAnchor component " +
             "is required to specify where to attach the next prefab to. " +
             "If no ProviderAnchor is found it will be attached directly to " +
             "the previous object which may or may not be desired.")]
    protected List<GameObject> prefabs = new List<GameObject>();

    public override RectTransform GetInputElement(string settingIdentifier, SettingsCollection settings)
    {
      if (prefabs == null || prefabs.Count < 1) { return null; }

      GameObject root = null;
      GameObject previousInstanceRoot = null;

      for (int i = 0; i < prefabs.Count; i++)
      {
        var prefab = prefabs[i];
        if (prefab == null) { continue; }

        // Create a new prefab instance of the current prefab
        var instance =
#if UNITY_EDITOR
          PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#else
          Instantiate(prefab) as GameObject;
#endif

        //if (i == 0)
        if (previousInstanceRoot == null)
        {
          // Setup the setting object for the root
          var settingObject = instance.AddOrGetComponent<SettingObject>();
          settingObject.Identifier = settingIdentifier;
          root = instance;
        }
        else
        {
          GameObject parent = previousInstanceRoot;
          if (previousInstanceRoot != null)
          {
            var anchor = previousInstanceRoot.GetComponentInChildren<ProviderAnchor>();
            if (anchor != null)
            {
              parent = anchor.gameObject;
            }
          }

          if (parent != null)
          {
            instance.transform.SetParent(parent.transform, false);
          }
        }

        previousInstanceRoot = instance;
      }
      return root != null ? root.GetComponent<RectTransform>() : null;
    }

    public override Type GetInputFieldParameterType(SettingsCollection settings) => null;

    public override Type GetInputFieldType(SettingsCollection settings) => null;
  }
}
