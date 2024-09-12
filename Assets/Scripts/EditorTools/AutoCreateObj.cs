using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
public class AutoCreateObj : EditorWindow
{   
    [MenuItem("EditorTool/AutoCreateObj")]
    private static void GenerateTextFields()
    {
        GameObject prefab = Resources.Load<GameObject>("Check");
        for(int i = 1;i<=25;i++)
        {
            GameObject textInstance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Text textComponent = textInstance.transform.Find("Label").GetComponent<Text>();
            string context = "垛位" + i.ToString();
            textComponent.text = context;
        }
    }

    [MenuItem("EditorTool/AutoRename")]
    private static void AutoRename()
    {
        


    }



}
#endif