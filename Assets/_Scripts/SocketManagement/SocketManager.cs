using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using System.Net;
using Champion;

public class SocketManager : MonoBehaviourSingleton<SocketManager>{

    private UdpClient client;
    private IPEndPoint hostEndPoint;
    private IPAddress serverIp;
    private bool isHost = false;
    private string _roomKey;
    public string roomKey { get { return _roomKey; } set
        {
            value.ToLower();
            _roomKey = value;
        }
    }

    private bool newCharUpdate = false;
    private List<string> newCharStrings = new List<string>();
    public bool IsClientRunningAndConnectedToRoom()
    {
        if(client.Client.Connected && roomKey != null)
        {
            return true;
        }
        return false;
    }
    protected override void SingletonAwake()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Offline
            return;
        }

        //serverIp = IPAddress.Parse("10.0.0.13");  ec2-34-205-19-225.compute-1.amazonaws.com  34.205.19.225

        //serverIp = Dns.GetHostAddresses("www.ec2-34-205-19-225.compute-1.amazonaws.com")[0];
        serverIp = IPAddress.Parse("34.205.19.225");
        hostEndPoint = new IPEndPoint(serverIp, 5555);
        client = new UdpClient(4444);
        

        //Application.runInBackground = true;
    }

    public void StartSocket()
    {
        if (client.Client.Connected)
            return;

        Debug.Log("Starting Socket");
        client.Connect(hostEndPoint);
        
        client.Client.Blocking = false;
        client.BeginReceive(new AsyncCallback(processDgram), client);
    }
    public void StopSocket()
    {
        if (isHost)
        {
            SendDgram("QUIT" + roomKey);
        }
        
        //client.Close();
        Debug.Log("Closed Socket");
    }


    void OnApplicationQuit()
    {
        StopSocket();

    }
    public void SendHost()
    {
        isHost = true;
        SendDgram("HOST");
    }
    public void SendKeepHostAlive()
    {
        Debug.Log("sending Keep Alive");
        string message = "KEEH" + roomKey;
        SendDgram(message);
    }
    public void SendRoomQuery()
    {
        string message = "RQRY" + roomKey;
        SendDgram(message);
    }
    public void SendPlayerUpdate()
    {
        string message = "MSGH" + roomKey;
        message += CharacterSerializerManager.SerializeCharacterForHost (CharacterManager.Instance.GetActiveCharacter());
        //message += "some char data";
        SendDgram(message);
    }
    private void SendDgram(string msg)
    {
        byte[] dgram = Encoding.UTF8.GetBytes(msg);
        //client.Client.Send(dgram);
        client.Send(dgram, dgram.Length);

    }
    private void processDgram(IAsyncResult res)
    {
        
        try
        {
            byte[] recieved = client.EndReceive(res, ref hostEndPoint);
            Debug.Log("recieved: " + Encoding.UTF8.GetString(recieved));
            //TODO sort data
            string packet = Encoding.UTF8.GetString(recieved);
            string packetType = packet.Substring(0, 4);
            
            switch (packetType) //Host Request Response
            {
                case "HRES":
                    Debug.Log("host response");
                    
                    roomKey = packet.Substring(4, 4);
                    Debug.Log(roomKey);
                    OnHostResponseFromServer();
                    break;
                case "MSGH": //Player Character Update
                    Debug.Log("Char update");
                    OnCharacterUpdateFromServer(packet.Substring(8, packet.Length -8));
                    break;
                case "QRES": //Room Query Resp
                    Debug.Log("Query Resp");
                    OnQueryResponseFromServer(packet.Substring(4, 1));

                    break;
            }
            client.BeginReceive(new AsyncCallback(processDgram), client);

        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
            throw ex;
            
        }
    }
    private void OnQueryResponseFromServer(string resp)
    {
        int response;
        if(int.TryParse(resp, out response))
        {
            if (response == 1)
            {
                Debug.Log("Room Key is Good");
                ScreenManager.Instance.UpdateJoinWithResponse(true);
            }
            else if (response == 0)
            {
                Debug.Log("Room Key is Not Good");
                ScreenManager.Instance.UpdateJoinWithResponse(false);
            }
        }
        
    }
    private void OnHostResponseFromServer()
    {
        ScreenManager.Instance.UpdateStartHost();
        //ScreenManager.Instance.SwitchToDMScreen();
    }
    private void OnCharacterUpdateFromServer(string characterpacket)
    {
        Debug.Log(characterpacket);
        CharacterDataMini updatedMini = CharacterSerializerManager.ReadCharacterMiniXML(characterpacket);
        Debug.Log(updatedMini.name);
        InitiativeManager.Instance.HandleCharacterUpdateFromServer(updatedMini);
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            SendPlayerUpdate();

        }
        
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            StartSocket();
            SendHost();


        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ScreenManager.Instance.SwitchToJoinScreen();
        }
    }
}
