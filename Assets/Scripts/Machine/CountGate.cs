using UnityEngine;

public class CountGate : MonoBehaviour
{   
    public GameObject conrrolBelt;
    public int passCount = 0;

    private int previousPassCount = 0;
    
    void Start()
    {
        // 初始化传送带速度
        conrrolBelt.GetComponent<LinearConveyor>().speed = 0.25f;
        this.transform.parent.GetComponent<BoxCollider>().enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bin"))
        {
            passCount++;
            UpdateGate();
        }
        
    }

    void Update()
    {
        // 检查 passCount 是否被外部修改
        if (passCount != previousPassCount)
        {
            UpdateGate();
        }
        
        // 更新 previousPassCount 以记录当前 passCount
        previousPassCount = passCount;
    }

    void UpdateGate()
    {
        if (passCount <= 13)
        {
            this.transform.parent.GetComponent<BoxCollider>().enabled = false;
            conrrolBelt.GetComponent<LinearConveyor>().speed = 0.15f;
        }
        else if (passCount >= 14)
        {
            this.transform.parent.GetComponent<BoxCollider>().enabled = true;
            conrrolBelt.GetComponent<LinearConveyor>().speed = 0;
        }
    }
}
