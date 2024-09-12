using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BatteryInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public int RFID = 123456;
    public int x,y,z;
    void Start()
    {
        x=y=z=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPosition(int x,int y,int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

}
