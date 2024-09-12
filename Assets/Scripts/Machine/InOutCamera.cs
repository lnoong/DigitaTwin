using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public class InOutCamera : MonoBehaviour
{

    public List<GameObject> wearhouse;
    private bool[] wearhouseStatus = new bool[8];

    public int minOfWearhouse,maxOfWearhouse;
    // Start is called before the first frame update
    void Start()
    {
        SyncInfo();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bin"))
        {
            bool foundX = false;
            int x = 0;
            for (int i = 0; i < 8; i++)
            {
                x = Random.Range(0, 8);
                if (wearhouseStatus[x] && wearhouse[x].GetComponent<Wearhouse>().storeNums < 180)
                {
                    foundX = true;
                    break;
                }
            }


            if (foundX)
            {
                if(maxOfWearhouse<9)
                    other.gameObject.GetComponent<BatteryInfo>().x = x;
                else
                    other.gameObject.GetComponent<BatteryInfo>().x = x+8;
                bool foundPosition = false;
                int y = 0;
                int z = 0;

                for (int j = 0; j < 180; j++)
                {
                    y = Random.Range(0, 19);
                    z = Random.Range(0, 9);
                    if (!wearhouse[x].GetComponent<Wearhouse>().isStore[y, z])
                    {
                        foundPosition = true;
                        break;
                    }
                }

                if (foundPosition)
                {
                    other.gameObject.GetComponent<BatteryInfo>().y = y;
                    other.gameObject.GetComponent<BatteryInfo>().z = z;

                }
                else
                {
                    Debug.LogWarning("未找到合适的位置来存储电池。");
                }
            }
            else
            {
                Debug.LogWarning("未找到合适的 wearhouse 位置来存储电池。");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        SyncInfo();
    }

    public void SyncInfo()
    {
        for (int i = 0; i < 8; i++)
        {
            wearhouseStatus[i] = wearhouse[i].GetComponent<Wearhouse>().isOpen;
        }
    }


    public void WearhouseOut(int x, int y, int z)
    {
        if(wearhouseStatus[x])
        {
            if(wearhouse[x].GetComponent<Wearhouse>().isStore[y,z])
            {
                
                wearhouse[x].GetComponent<Wearhouse>().WearhouseOut(y,z);
            }
            else
            {
                Debug.LogWarning("库位空");
            }          
  
        }
        else
        {
            Debug.LogWarning("立库关");
        }
    }


}
#if  UNITY_EDITOR
[CustomEditor(typeof(InOutCamera))]
public class MyScriptEditor : Editor
{
    // 定义参数字段
    public int x;
    public int y;
    public int z;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // 获取目标脚本
        InOutCamera myScript = (InOutCamera)target;

        // 创建输入字段
        x = EditorGUILayout.IntField("X", x);
        y = EditorGUILayout.IntField("Y", y);
        z = EditorGUILayout.IntField("Z", z);

        // 创建按钮
        if (GUILayout.Button("Call WearhouseOut"))
        {
            myScript.WearhouseOut(x, y, z);
        }
    }
}
#endif