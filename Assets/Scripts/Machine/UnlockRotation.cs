using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRotation : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bin"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                rb.isKinematic = false;
            }   
        }
    }
}
