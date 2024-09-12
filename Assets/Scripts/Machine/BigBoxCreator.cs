using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BigBoxCreator : MonoBehaviour
{
    public GameObject targetObject;
    private GameObject prefabBigBox;


    public bool isOpen = false;
    public float time = 1.0f;
    
    void Start()
    {
        prefabBigBox = Resources.Load<GameObject>("BigBox");
        StartCoroutine(SpawnProduct());
    }

    IEnumerator SpawnProduct()
    {
        while (true)
        {
            if(isOpen)
            {
                GameObject product;
                product = Instantiate(prefabBigBox);
                Vector3 spawnPosition = new Vector3(transform.position.x-1.5f, 1.1f, targetObject.transform.position.z);
                product.transform.position = spawnPosition;
                yield return new WaitForSeconds(time);

            }
            yield return null;
        }
    }

}
