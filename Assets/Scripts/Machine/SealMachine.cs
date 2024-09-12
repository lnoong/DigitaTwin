using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealMachine : MonoBehaviour
{
    public GameObject belt;
    public bool isOpen = false;
    public float time = 1;
    private LinearConveyor playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = belt.GetComponent<LinearConveyor>();
        animator1 = transform.GetChild(4).GetComponent<Animator>();
        animator2 = transform.GetChild(7).GetComponent<Animator>();
        StartCoroutine(SpawnProduct());
    }
    private Animator animator1,animator2;
    // Start is called before the first frame update
    IEnumerator SpawnProduct()
    {
        while (true)
        {
            if(isOpen)
            {
                animator1.SetBool("isOpen", true);
                animator1.SetFloat("time",1/time);
                animator2.SetBool("isOpen", true);
                animator2.SetFloat("time",1/time);
                playerController.speed = 1/time;
            }
            else
            {
                animator1.SetBool("isOpen", false);
                animator2.SetBool("isOpen", false);
            }
            yield return new WaitForSeconds(time);
        }
    }

}
