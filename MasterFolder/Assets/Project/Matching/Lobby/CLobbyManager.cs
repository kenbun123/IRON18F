using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CLobbyManager :NetworkLobbyManager {


    private int m_NowConnectPlayerNum=0;

    
    bool m_allPlayerRedy = false;

    public bool IsDebug;

    public GameObject[] m_AllLobbyPlayer;

    public int ConnectPlayerNum {
        get { return m_NowConnectPlayerNum; }

        
    }
    

    
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        var cc = lobbyPlayer.GetComponent<CLobbyPlayer>();
        var aa = lobbyPlayer.GetComponent<CLobbySelect>();
        var work = gamePlayer.GetComponent<CPlayer>();

        work.m_meType = aa.SyncSelectType;
        work.m_id = cc.m_SyncId;

        return true;
    }

    public override void OnLobbyServerConnect(NetworkConnection conn)
    {
        m_NowConnectPlayerNum++;
    }

    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        m_NowConnectPlayerNum--;
    }

    public override void OnLobbyStartServer()
    {
        m_NowConnectPlayerNum = 0;
    }

    public override void OnLobbyServerPlayersReady()
    {
        m_allPlayerRedy = true;
        ServerChangeScene("Game");
        //FadeManager.Instance.LoadLevel(SCENE_RAVEL.GAME,0.5f,null,ServerChangeScene);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MATCHING"||
            SceneManager.GetActiveScene().name == "Matching")
        {
            UpdateMatchingScene();

            gameObject.GetComponent<CNetWorkManagerHUD>().enabled = true;

        }
        else {
            gameObject.GetComponent<CNetWorkManagerHUD>().enabled = false;
        }
        

    }

    void UpdateMatchingScene()
    {
        if (m_AllLobbyPlayer.Length <= m_NowConnectPlayerNum)
        {
            m_AllLobbyPlayer = null;
            m_AllLobbyPlayer = GameObject.FindGameObjectsWithTag("LobbyPlayer");
        }

    }

    public override void OnStartServer()
    {
        base.OnStartServer();


    }
}
