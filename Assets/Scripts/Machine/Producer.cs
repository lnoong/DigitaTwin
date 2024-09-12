using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Producer : MonoBehaviour
{
    public GameObject targetObject;
    public float probability = 0.5f;
    private GameObject prefabObjectA;
    private GameObject prefabObjectB;
    private GameObject prefabBattery;


    public bool isOpen = false;
    public float time = 1.0f;
    public char direction = 'x';
    
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        prefabObjectA = Resources.Load<GameObject>("PlasticBinGreen");
        prefabObjectB = Resources.Load<GameObject>("PlasticBinRed");
        prefabBattery = Resources.Load<GameObject>("Battery");

        animator = transform.GetChild(3).GetComponent<Animator>();
        StartCoroutine(SpawnProduct());
    }

    IEnumerator SpawnProduct()
    {
        while (true)
        {
            if(isOpen)
            {
                GameObject product;
                float randomValue = Random.Range(0f, 1f);
                if (randomValue < probability)
                {
                    product = Instantiate(prefabObjectA);
                }
                else
                {
                    product = Instantiate(prefabObjectB);
                }
                int batteryNum = Random.Range(1,4);
                for(int i = 0;i<batteryNum;i++)
                {
                    GameObject childObject = Instantiate(prefabBattery);
                    childObject.transform.SetParent(product.transform.GetChild(2));
                    childObject.transform.localPosition = new Vector3(0, 0.04f + i * 0.07f, 0);
                }

                Vector3 spawnPosition = new Vector3(0, 1, 0);
                if(direction == 'x')
                {
                    spawnPosition = new Vector3(targetObject.transform.position.x, 1, transform.position.z); // 根据需要调整高度
                }
                else if(direction == 'z')
                {
                    spawnPosition = new Vector3(transform.position.x, 1, targetObject.transform.position.z); // 根据需要调整高度

                }
                product.transform.position = spawnPosition;
            }
            yield return new WaitForSeconds(time);
        }
    }

    public void UpdateParameters(bool isOpen,float time)
    {
        this.isOpen = isOpen;
        this.time = time;
        animator.SetBool("isOpen", isOpen);
        animator.SetFloat("time",1/time);
    }

}
