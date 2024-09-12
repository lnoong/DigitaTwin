using System.Collections;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject sourceBox;
    public GameObject bigBoxCheck;

    public GameObject targetBox;

    public int objCount;
    public float time = 1.0f;
    public bool isOpen = false;
    public GameObject OBJ;
    private bool leftOrRight = false;

    void Start()
    {
        StartCoroutine(ActivateAfterDelay());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bin"))
        {
            if(objCount == 0)
            {
                OBJ = new GameObject("OBJ");
                OBJ.transform.position = sourceBox.transform.position;              
            }
            other.gameObject.transform.SetParent(OBJ.transform, true);
            objCount++;
        }
    }



    IEnumerator ActivateAfterDelay()
    {
        while (true)
        {
            targetBox = bigBoxCheck.GetComponent<BigBox>().bigBox;
            if(targetBox != null&&objCount==7 &&isOpen)
            {
                
                for(int i= 0;i< OBJ.transform.childCount;i++)
                {
                    OBJ.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = true;
                }
                if(!leftOrRight)
                {
                    objCount=0;
                    OBJ.gameObject.transform.position = targetBox.transform.position+new Vector3(-0.08f,0.03f,0);
                    OBJ.gameObject.transform.SetParent(targetBox.transform,true);
                    yield return new WaitForSeconds(time);
                    leftOrRight = !leftOrRight;
                }
                else if(leftOrRight)
                {
                    objCount=0;
                    OBJ.gameObject.transform.position = targetBox.transform.position+new Vector3(0.08f,0.03f,0);
                    OBJ.gameObject.transform.SetParent(targetBox.transform,true);
                    yield return new WaitForSeconds(time);
                    bigBoxCheck.GetComponent<BigBox>().ResetBelt();
                    leftOrRight = !leftOrRight;
                }
            }
            yield return null;
        }
    }
}
