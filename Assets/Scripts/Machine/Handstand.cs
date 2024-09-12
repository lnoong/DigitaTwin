using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handstand : MonoBehaviour
{
    // Start is called before the first frame update


    public int handstandCount = 0;
    public GameObject countGate;
    public float time = 5.0f;
    public bool isOpen = false;
    void Start()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bin"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0,0,0);
            other.transform.rotation = Quaternion.Euler(0, 90, 90);

            handstandCount++;
            if(handstandCount <= 7)
            {
                other.transform.position = new Vector3(transform.GetChild(4).position.x, transform.GetChild(4).position.y+1f, transform.GetChild(4).position.z+(handstandCount-4)*0.05f);
            }
            else
            {
                other.transform.position = new Vector3(transform.GetChild(4).position.x, transform.GetChild(4).position.y+1f, transform.GetChild(4).position.z+(handstandCount-7-4)*0.05f);
            }
        }
    }


    IEnumerator ActivateAfterDelay()
    {
        while (true)
        {
            if(handstandCount==14&&isOpen)
            {
                yield return new WaitForSeconds(time);
                countGate.GetComponent<CountGate>().passCount = 0;
                handstandCount = 0;
            }
            yield return null;
        }
    }
}
