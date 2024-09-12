using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class Wearhouse : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isWork = false;
    private bool isTriggered = false;
    public GameObject inBelt;
    public GameObject outBelt;
    public GameObject cameraIn;

    private GameObject battery;
    public float time = 1.0f;
    public bool isOpen = false;
    public bool[,] isStore = new bool[20,9];
    public int storeNums = 0;
    public int wearhouseId = 0;
    void Start()
    {
        for (int i = 0; i < isStore.GetLength(0); i++)
        {
            for (int j = 0; j < isStore.GetLength(1); j++)
            {
                isStore[i, j] = false;
            }
        }
        StartCoroutine(ActivateAfterDelay());
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bin") && !isWork && other.GetComponent<BatteryInfo>().x==wearhouseId)
        {
            battery = other.gameObject;
            isTriggered = true;
            inBelt.GetComponent<LinearConveyor>().speed = 0;
        }
    }

    IEnumerator ActivateAfterDelay()
    {
        while (true)
        {
            if (isOpen && isTriggered && !isWork && battery.GetComponent<BatteryInfo>().x==wearhouseId)
            {
                isWork = true;
                int y = battery.GetComponent<BatteryInfo>().y;
                int z = battery.GetComponent<BatteryInfo>().z;
                //Vector3 targetPosition = new Vector3(transform.localPosition.x+((z-4)*0.12f),transform.localPosition.y+(y*0.15f+0.05f),transform.localPosition.z);
                //battery.transform.position = targetPosition;
                Transform parent = transform.GetChild(y).transform.GetChild(z);
                battery.transform.parent = parent;
                battery.transform.localPosition = new Vector3(0,0.05f,0);
                battery.transform.localRotation = Quaternion.Euler(0,0,0);
                isStore[y,z] = true;
                storeNums++;
                yield return new WaitForSeconds(time);
                inBelt.GetComponent<LinearConveyor>().speed = 0.25f;
                isWork = false;
                isTriggered = false;
            }
            yield return null;
        }
    }

    public void WearhouseOut(int y,int z)
    {
        Transform outBattery = transform.GetChild(y).transform.GetChild(z).GetChild(0);
        outBattery.transform.parent = null;
        outBattery.transform.position = new Vector3(outBattery.transform.position.x, outBelt.transform.position.y + 0.05f, outBelt.transform.position.z);
        storeNums--;
        isStore[y, z] = false;
    }

}
