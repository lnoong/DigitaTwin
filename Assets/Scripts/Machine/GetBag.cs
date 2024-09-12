using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBag : MonoBehaviour
{
    // Start is called before the first frame update
private bool isWork = false;
private bool isTriggered = false;
public GameObject inBelt;
public GameObject outBelt;
private GameObject plasticBin;
public float time = 1.0f;
public bool isOpen = true;

void Start()
{
    StartCoroutine(ActivateAfterDelay());
}

void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("BinA") && !isWork)
    {
        plasticBin = other.gameObject;
        isTriggered = true;
    }
}

IEnumerator ActivateAfterDelay()
{
   while (true)
        {
            if (isOpen && isTriggered && !isWork)
            {
                isWork = true;
                plasticBin.tag = "Untagged";

                Vector3 positionWork = new Vector3(transform.position.x, plasticBin.transform.position.y, transform.position.z);
                Vector3 positionIn = new Vector3(transform.position.x, inBelt.transform.position.y + 0.05f, plasticBin.transform.position.z);
                Vector3 positionOut = new Vector3(transform.position.x, outBelt.transform.position.y + 0.05f, outBelt.transform.position.z);

                plasticBin.transform.position = positionWork;
                Transform batteriesParent = plasticBin.transform.GetChild(2);
                for (int i = batteriesParent.childCount - 1; i >= 0; i--)
                {
                    GameObject battery = batteriesParent.GetChild(i).gameObject;
                    battery.transform.SetParent(null);
                    yield return new WaitForSeconds(time);
                    battery.transform.position = positionIn;
                }
                plasticBin.transform.position = positionOut;
                isWork = false;
                isTriggered = false;
            }
            yield return null;
        }
}

    public void UpdateParameters(bool isOpen,float time)
    {
        this.isOpen = isOpen;
        this.time = time;
    }
}
