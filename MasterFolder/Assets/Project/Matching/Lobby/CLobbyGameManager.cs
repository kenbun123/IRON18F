using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CLobbyGameManager : NetworkBehaviour {

    [SyncVar]
    public bool CanSelectGhost;

    [SyncVar]
    public int m_SelectHuman;

    [SyncVar]
    public int m_NowConnect;


    private CLobbyManager m_lobbyManager;
    void Start()
    {
        CanSelectGhost = true;

        m_lobbyManager = GameObject.Find("LobbyManager").GetComponent<CLobbyManager>();
    }

    void Update()
    {
        m_NowConnect = m_lobbyManager.ConnectPlayerNum;

    }
}
