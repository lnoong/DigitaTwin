using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignalSystem : MonoBehaviour
{
    public Canvas mainUI;
    public TMP_InputField portIn;

    public ScrollRect Transfer;
    public ScrollRect Artificial;
    public ScrollRect Warehouse;
    public ScrollRect Palletizing;
    public Dictionary<string, bool> isOpen;
    public Dictionary<string, float> time;
    
    public GameObject[] producers;
    public GameObject[] getbags;
    public GameObject[] sealMachines;
    public GameObject[] wearhouses;
    public GameObject bigBox;
    public GameObject handstand;

    public GameObject robot;

    public GameObject pallet;


    private GameObject prefabBattery;



    void Start()
    {
        isOpen = new Dictionary<string, bool>();
        time = new Dictionary<string, float>();
        LoadParameters();
        SetParameters();
        prefabBattery = Resources.Load<GameObject>("Battery");
    }

 


    public void LoadParameters()
    {
        isOpen["transfer1"] = false;    time["transfer1"] = 1f;
        isOpen["transfer2"] = false;    time["transfer2"] = 1f;
        isOpen["transfer3"] = false;    time["transfer3"] = 1f;
        isOpen["transfer4"] = false;    time["transfer4"] = 1f;
        isOpen["transfer5"] = false;    time["transfer5"] = 1f;
        isOpen["transfer6"] = false;    time["transfer6"] = 1f;
        isOpen["transfer7"] = false;    time["transfer7"] = 1f;
        isOpen["transfer8"] = false;    time["transfer8"] = 1f;
        isOpen["transfer9"] = false;    time["transfer9"] = 1f;
        isOpen["transfer10"] = false;    time["transfer10"] = 1f;
        isOpen["transfer11"] = false;    time["transfer11"] = 1f;


        isOpen["artificial1"] = false;    time["artificial1"] = 1f;
        isOpen["artificial2"] = false;    time["artificial2"] = 1f;
        isOpen["artificial3"] = false;    time["artificial3"] = 1f;
        isOpen["artificial4"] = false;    time["artificial4"] = 1f;
        isOpen["artificial5"] = false;    time["artificial5"] = 1f;
        isOpen["artificial6"] = false;    time["artificial6"] = 1f;
        isOpen["artificial7"] = false;    time["artificial7"] = 1f;
        isOpen["artificial8"] = false;    time["artificial8"] = 1f;
        isOpen["artificial9"] = false;    time["artificial9"] = 1f;
        isOpen["artificial10"] = false;   time["artificial10"] = 1f;
        isOpen["artificial11"] = false;   time["artificial11"] = 1f;


        isOpen["warehouse1"] = false;    time["warehouse1"] = 1f;
        isOpen["warehouse2"] = false;    time["warehouse2"] = 1f;
        isOpen["warehouse3"] = false;    time["warehouse3"] = 1f;
        isOpen["warehouse4"] = false;    time["warehouse4"] = 1f;
        isOpen["warehouse5"] = false;    time["warehouse5"] = 1f;
        isOpen["warehouse6"] = false;    time["warehouse6"] = 1f;
        isOpen["warehouse7"] = false;    time["warehouse7"] = 1f;
        isOpen["warehouse8"] = false;    time["warehouse8"] = 1f;
        isOpen["warehouse9"] = false;    time["warehouse9"] = 1f;
        isOpen["warehouse10"] = false;    time["warehouse10"] = 1f;
        isOpen["warehouse11"] = false;    time["warehouse11"] = 1f;
        isOpen["warehouse12"] = false;    time["warehouse12"] = 1f;
        isOpen["warehouse13"] = false;    time["warehouse13"] = 1f;
        isOpen["warehouse14"] = false;    time["warehouse14"] = 1f;
        isOpen["warehouse15"] = false;    time["warehouse15"] = 1f;
        isOpen["warehouse16"] = false;    time["warehouse16"] = 1f;


        isOpen["palletizing1"] = false;    time["palletizing1"] = 1f;
        isOpen["palletizing2"] = false;    time["palletizing2"] = 1f;
        isOpen["palletizing3"] = false;    time["palletizing3"] = 1f;
        isOpen["palletizing4"] = false;    time["palletizing4"] = 1f;
        isOpen["palletizing5"] = false;    time["palletizing5"] = 1f;    
        isOpen["palletizing6"] = false;    time["palletizing6"] = 1f;    
        isOpen["palletizing7"] = false;    time["palletizing7"] = 1f;    
        isOpen["palletizing8"] = false;    time["palletizing8"] = 1f;    
        isOpen["palletizing9"] = false;    time["palletizing9"] = 1f;    
        isOpen["palletizing10"] = false;   time["palletizing10"] = 1f;   
        isOpen["palletizing11"] = false;   time["palletizing11"] = 1f;   
        isOpen["palletizing12"] = false;   time["palletizing12"] = 1f;   
        isOpen["palletizing13"] = false;   time["palletizing13"] = 1f;
        isOpen["palletizing14"] = false;   time["palletizing14"] = 1f;
        isOpen["palletizing15"] = false;   time["palletizing15"] = 1f;
        isOpen["palletizing16"] = false;   time["palletizing16"] = 1f;
        isOpen["palletizing17"] = false;   time["palletizing17"] = 1f;
        isOpen["palletizing18"] = false;   time["palletizing18"] = 1f;
        isOpen["palletizing19"] = false;   time["palletizing19"] = 1f;
        isOpen["palletizing20"] = false;   time["palletizing20"] = 1f;
        isOpen["palletizing21"] = false;   time["palletizing21"] = 1f;
        isOpen["palletizing22"] = false;   time["palletizing22"] = 1f;
        isOpen["palletizing23"] = false;   time["palletizing23"] = 1f;
        isOpen["palletizing24"] = false;   time["palletizing24"] = 1f;
        isOpen["palletizing25"] = false;   time["palletizing25"] = 1f;
        isOpen["palletizing26"] = false;   time["palletizing26"] = 1f;
        isOpen["palletizing27"] = false;   time["palletizing27"] = 1f;
        isOpen["palletizing28"] = false;   time["palletizing28"] = 1f;
        isOpen["palletizing29"] = false;   time["palletizing29"] = 1f;

    }

    public void  GetParameters()
    {
        List<GameObject> transferElm = GetAllUIElements(Transfer.content.gameObject);
        List<GameObject> artificialElm = GetAllUIElements(Artificial.content.gameObject);
        List<GameObject> warehouseElm = GetAllUIElements(Warehouse.content.gameObject);
        List<GameObject> palletizingElm = GetAllUIElements(Palletizing.content.gameObject);
        // 示例处理：获取文本框和复选框的数据
        int isOpenNum = 1;
        int timeNum = 1;
        for (int i = 0; i < transferElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = transferElm[i];

            string key1  = "transfer" + isOpenNum.ToString();
            string key2  = "transfer" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                isOpen[key1] = toggle.isOn;
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                time[key2] = float.Parse(inputField.text);
                timeNum++;
                continue;
            }

        }
        for (int i = 0; i < artificialElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = artificialElm[i];

            string key1  = "artificial" + isOpenNum.ToString();
            string key2  = "artificial" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                isOpen[key1] = toggle.isOn;
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                time[key2] = float.Parse(inputField.text);
                timeNum++;
                continue;
            }

        }
        for (int i = 0; i < warehouseElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = warehouseElm[i];

            string key1  = "warehouse" + isOpenNum.ToString();
            string key2  = "warehouse" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                isOpen[key1] = toggle.isOn;
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                time[key2] = float.Parse(inputField.text);
                timeNum++;
                continue;
            }

        }
        for (int i = 0; i < palletizingElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = palletizingElm[i];

            string key1  = "palletizing" + isOpenNum.ToString();
            string key2  = "palletizing" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                isOpen[key1] = toggle.isOn;
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                time[key2] = float.Parse(inputField.text);
                timeNum++;
                continue;
            }

        }

    }
    public void  SetParameters()
    {
        List<GameObject> transferElm = GetAllUIElements(Transfer.content.gameObject);
        List<GameObject> artificialElm = GetAllUIElements(Artificial.content.gameObject);
        List<GameObject> warehouseElm = GetAllUIElements(Warehouse.content.gameObject);
        List<GameObject> palletizingElm = GetAllUIElements(Palletizing.content.gameObject);
        int isOpenNum = 1;
        int timeNum = 1;
        for (int i = 0; i < transferElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = transferElm[i];

            string key1  = "transfer" + isOpenNum.ToString();
            string key2  = "transfer" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.isOn = isOpen[key1];
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                inputField.text = time[key2].ToString();
                timeNum++;
                continue;
            }

        }
        for (int i = 0; i < artificialElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = artificialElm[i];

            string key1  = "artificial" + isOpenNum.ToString();
            string key2  = "artificial" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.isOn = isOpen[key1];
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                inputField.text = time[key2].ToString();
                timeNum++;
                continue;
            }

        }


        for (int i = 0; i < warehouseElm.Count; i++)
        {
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = warehouseElm[i];

            string key1  = "warehouse" + isOpenNum.ToString();
            string key2  = "warehouse" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.isOn = isOpen[key1];
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                inputField.text = time[key2].ToString();
                timeNum++;
                continue;
            }

        }
        for (int i = 0; i < palletizingElm.Count; i++)
        {
            
            if(i == 0)
            {
                isOpenNum = 1;
                timeNum = 1;
            }

            GameObject element = palletizingElm[i];

            string key1  = "palletizing" + isOpenNum.ToString();
            string key2  = "palletizing" + timeNum.ToString();

            Toggle toggle = element.GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.isOn = isOpen[key1];
                isOpenNum++;
                continue;
            }
            TMP_InputField inputField = element.GetComponent<TMP_InputField>();
            if (inputField != null)
            {
                inputField.text = time[key2].ToString();
                timeNum++;
                continue;
            }

        }

    }
    List<GameObject> GetAllUIElements(GameObject parent)
    {
        List<GameObject> uiElements = new List<GameObject>();

        foreach (Transform child in parent.transform)
        {
            uiElements.Add(child.gameObject);
            // 递归获取子对象中的UI元素
            uiElements.AddRange(GetAllUIElements(child.gameObject));
        }

        return uiElements;
    }
    
    public void ApplyParameters()
    {
        
        for(int i=0;i<producers.Length;i++)
        {
            GameObject producer = producers[i];
            string key = "transfer" + (i+1).ToString();
            Producer producerControl = producer.GetComponent<Producer>();
            if(producerControl != null)
            {
                producerControl.UpdateParameters(isOpen[key],time[key]);
            }
        }



        for(int i=0;i<getbags.Length;i++)
        {
            GameObject getbag = getbags[i];
            string key = "artificial" + (i+1).ToString();
            GetBag getbagControl = getbag.GetComponent<GetBag>();
            if(getbagControl != null)
            {    
                getbagControl.isOpen = isOpen[key];
                getbagControl.time = time[key];
            }
        }
        for(int i=0;i<sealMachines.Length;i++)
        {
            GameObject sealMachine = sealMachines[i];
            string key = "artificial" + (i+10).ToString();
            SealMachine sealMachineControl = sealMachine.GetComponent<SealMachine>();
            if(sealMachineControl!=null)
            {
                sealMachineControl.isOpen = isOpen[key];
                sealMachineControl.time = 1.0f;
            }
        }
        for(int i=0;i<wearhouses.Length;i++)
        {
            GameObject wearhouse = wearhouses[i];
            string key = "warehouse" + (i+1).ToString();
            Wearhouse wearhouseControl = wearhouse.GetComponent<Wearhouse>();
            if(wearhouseControl != null)
            {    
                wearhouseControl.isOpen = isOpen[key];
                wearhouseControl.time = time[key];
            }
        }
        
        BigBoxCreator bigBoxCreatorCtrl = bigBox.GetComponent<BigBoxCreator>();
        if(bigBoxCreatorCtrl != null)
        {    
            string key = "palletizing1";
            bigBoxCreatorCtrl.isOpen = isOpen[key];
            bigBoxCreatorCtrl.time = time[key];
        }

        Handstand handstandCtrl = handstand.GetComponent<Handstand>();
        if(handstandCtrl != null)
        {    
            string key = "palletizing2";
            handstandCtrl.isOpen = isOpen[key];
            handstandCtrl.time = time[key];
        }

        Robot robotCtrl = robot.GetComponent<Robot>();
        if(robotCtrl != null)
        {    
            string key = "palletizing3";
            robotCtrl.isOpen = isOpen[key];
            robotCtrl.time = time[key];
        }

    }
    public void WearhouseOut()
    {
        for(int i = 0;i<14;i++)
        {
            GameObject childObject = Instantiate(prefabBattery);
            childObject.transform.position = new Vector3(38f+(0.2f*i),2.02f,35.6f);
        }
    }

}
