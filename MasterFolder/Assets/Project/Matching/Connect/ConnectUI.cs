using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class ConnectUI : MonoBehaviour {



    public string m_ServerIp;
    NetworkClient myClient;

    bool IsSelect = false;


    public void SetupServer()
    {
        IsSelect =NetworkManager.singleton.StartServer();
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnection);

    }



    public void SetupClient()
    {
        var client = NetworkManager.singleton.StartClient();
        client.Connect(m_ServerIp, 80);
        client.RegisterHandler(MsgType.Disconnect, OnConnected);
    }

    private void OnConnection(NetworkMessage msg)
    {
        Debug.Log("Server: connection happen");
    }

    private void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Client: connected to server ");
    }
    void OnGUI()
    {
        if (IsSelect == false)
        {
            if (GUI.Button(new Rect(10, 10, 100, 70), "Start Server"))
            {
                SetupServer();

            }


            if (GUI.Button(new Rect(10, 60, 100, 70), "Start Client"))
            {
                SetupClient();

            }

        }


    }


}
