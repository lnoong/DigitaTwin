using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Net;
using System;

public class ConnectTCP : MonoBehaviour
{
    private TcpListener server;
    private Thread serverThread;
    private bool isRunning;
    public int port = 8888;
    private Dictionary<int, TcpClient> clients = new Dictionary<int, TcpClient>();
    public Canvas mainUI;
    public TMP_InputField portIn;
    public SignalSystem signalSystem;


    public GameObject inCamera1;
    public GameObject inCamera2;

    private InOutCamera inOutCamera1;
    private InOutCamera inOutCamera2;


    void Start()
    {
        signalSystem = transform.GetComponent<SignalSystem>();
        inOutCamera1 = inCamera1.GetComponent<InOutCamera>();
        inOutCamera2 = inCamera2.GetComponent<InOutCamera>();
    }

    public void StartServer()
    {
        if(!isRunning)
        {
            GameObject target = GameObject.Find("connecting");
            target.transform.GetChild(0).GetComponent<Text>().text = "断开";
            this.port = int.Parse(portIn.text);
            serverThread = new Thread(new ThreadStart(StartListening));
            serverThread.IsBackground = true;
            isRunning = true;
            serverThread.Start();
        }
        else if(isRunning)
        {
            GameObject target = GameObject.Find("connecting");
            target.transform.GetChild(0).GetComponent<Text>().text = "监听";
            isRunning = false;
            server.Stop();
            serverThread.Abort();
        }
    }

    private void StartListening()
    {
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Debug.Log("Server started on port " + port);

        while (isRunning)
        {
            if (server.Pending())
            {
                TcpClient client = server.AcceptTcpClient();
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
        }
    }

    private void HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int deviceId = -1;

        while (client.Connected)
        {
            if (stream.DataAvailable)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Debug.Log("Received: " + message);
                



                if(message.Contains("\r\n"))
                {
                    while (message.Contains("\r\n"))
                    {
                        int endIndex = message.IndexOf("\r\n");
                        string messageOne = message.Substring(0, endIndex);
                        int X = int.Parse(message.Substring(0, 2));
                        int Y = int.Parse(message.Substring(2, 2));
                        int Z = int.Parse(message.Substring(4, 2));
                        int State = int.Parse(message.Substring(6, 2));
                        if(X<8)
                        {
                            inOutCamera1.WearhouseOut(X,Y,Z);
                        }
                        else
                        {
                            inOutCamera2.WearhouseOut(X,Y,Z);
                        }
                        Debug.Log("Received message: " + messageOne);
                        message = message.Substring(endIndex + 2);
                    }
                    continue;
                }

                
                if (message.Length == 8)
                {
                    int deviceType = int.Parse(message.Substring(0, 1));
                    int deviceIdNum = int.Parse(message.Substring(1, 2));
                    int switchState = int.Parse(message.Substring(3, 1));
                    int usageTime = int.Parse(message.Substring(4, 4));

                    deviceId = deviceType * 100 + deviceIdNum; // 生成唯一设备ID

                    if (!clients.ContainsKey(deviceId))
                    {
                        clients.Add(deviceId, client);
                    }

                    HandleMessage(deviceId, deviceType, deviceIdNum, switchState, usageTime);
                }
                else
                {
                    Debug.LogError("MessageError");
                }
            }
            Thread.Sleep(10);
        }

        if (deviceId != -1 && clients.ContainsKey(deviceId))
        {
            clients.Remove(deviceId);
        }

        client.Close();
    }

    private void HandleMessage(int deviceId, int deviceType, int deviceIdNum, int switchState, int usageTime)
    {
        Debug.Log($"Message from device {deviceId}: " +
                  $"Type: {deviceType}, " +
                  $"ID: {deviceIdNum}, " +
                  $"Switch State: {switchState}, " +
                  $"Usage Time: {usageTime}");

        if(deviceType==1)
        {
            string type = "transfer";
            string index = deviceIdNum.ToString();
            signalSystem.isOpen[type+index] = Convert.ToBoolean(switchState);
            signalSystem.time[type+index] = usageTime;
        }
        else if(deviceType==2)
        {
            string type = "artificial";
            string index = deviceIdNum.ToString();
            signalSystem.isOpen[type+index] = Convert.ToBoolean(switchState);
            signalSystem.time[type+index] = usageTime;
        }
        else if(deviceType==3)
        {
            string type = "warehouse";
            string index = deviceIdNum.ToString();
            signalSystem.isOpen[type+index] = Convert.ToBoolean(switchState);
            signalSystem.time[type+index] = usageTime;
        }
        else if(deviceType==4)
        {
            string type = "palletizing";
            string index = deviceIdNum.ToString();
            signalSystem.isOpen[type+index] = Convert.ToBoolean(switchState);
            signalSystem.time[type+index] = usageTime;
        }
        else
        {
            Debug.Log("MsgError");
        }
    }

    public void SendMessageToClient()
    {
        foreach (var client in clients)
        {
            int type = client.Key/100;
            int index = ((client.Key / 10) % 10) + (client.Key % 10);
            string message = "";
            if(type == 1)
            {   
                string keyType = "transfer"+index.ToString();
                string isOpen = signalSystem.isOpen[keyType]?"1":"0";
                string time =  ((int)signalSystem.time[keyType]).ToString("D4");
                message = type.ToString("D1")+index.ToString("D2")+isOpen+time;
            }
            else if(type == 2)
            {
                string keyType = "artificial"+index.ToString();
                string isOpen = signalSystem.isOpen[keyType]?"1":"0";
                string time =  ((int)signalSystem.time[keyType]).ToString("D4");
                message = type.ToString("D1")+index.ToString("D2")+isOpen+time;
            }
            else if(type == 3)
            {
                string keyType = "warehouse"+index.ToString();
                string isOpen = signalSystem.isOpen[keyType]?"1":"0";
                string time =  ((int)signalSystem.time[keyType]).ToString("D4");
                message = type.ToString("D1")+index.ToString("D2")+isOpen+time;
            }           
            else if(type == 4)
            {
                string keyType = "palletizing"+index.ToString();
                string isOpen = signalSystem.isOpen[keyType]?"1":"0";
                string time =  ((int)signalSystem.time[keyType]).ToString("D4");
                message = type.ToString("D1")+index.ToString("D2")+isOpen+time;
            }
            else
            {
                Debug.Log("NoDevice");
            }
            NetworkStream stream = client.Value.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);        
        }
    }

    public void StopServer()
    {
        isRunning = false;
        server.Stop();
        serverThread.Abort();
    }
}
