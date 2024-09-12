    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class BigBox : MonoBehaviour
    {
        public GameObject belt;
        public GameObject bigBox;
        void Start()
        {
        }
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("BigBox"))
            {
                bigBox = other.gameObject;
                belt.GetComponent<LinearConveyor>().speed = 0;
            }
        }


        public void ResetBelt()
        {
            bigBox = null;
            belt.GetComponent<LinearConveyor>().speed =0.25f;
        }

    }
