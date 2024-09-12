using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFID : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject InBelt;
    public GameObject OutBelt;

    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BinB"))
        {
            Vector3 outPosition = OutBelt.transform.position;
            outPosition.y += 0.05f;
            other.gameObject.transform.position = outPosition;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateParameters(bool isOpen,float time)
    {
        InBelt.GetComponent<LinearConveyor>().speed = 1/time;
    }
}
