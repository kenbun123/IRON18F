using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class CLobbySelect : NetworkBehaviour {

    public enum CONTROL_TYPE
    {
        NONE = 0,
        HUMAN,
        GHOST
    };

    [SyncVar]
    public int SyncSelectType;

    [SyncVar]
    public bool SyncToRedy;

    private CLobbyGameManager m_lobbyGameManager;



    // Use this for initialization
    void Start() {
        m_lobbyGameManager = GameObject.Find("LobbyGameManager").GetComponent<CLobbyGameManager>();


        if (isLocalPlayer)
        {
            Cmd_UpdateSelect((int)CONTROL_TYPE.HUMAN);
        }




    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) return;
        
            UpdateLocalPlayer();


    }


    void UpdateLocalPlayer()
    {
        if (!gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin)
        {
            Control();
        }


        if (!Input.GetKeyDown(KeyCode.X)) return;

        if (SyncToRedy)
        {
            Cmd_UpdateToReady(false);

            gameObject.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
        }
        else {

            Cmd_UpdateToReady(true);
            gameObject.GetComponent<NetworkLobbyPlayer>().SendNotReadyToBeginMessage();

        }
    }




    bool m_isHumanSelect = false;
    void Control()
    {
        if (Input.GetKeyDown(KeyCode.A) && m_isHumanSelect)
        {
            m_isHumanSelect = false;
           // CSoundManager.Instance.PlaySE(EAudioList.SE_CandyThrow);
        }
        else if (Input.GetKeyDown(KeyCode.D) && !m_isHumanSelect)
        {
            m_isHumanSelect = true;
            //CSoundManager.Instance.PlaySE(EAudioList.SE_CandyThrow);
        }
        if (m_isHumanSelect)
            Cmd_UpdateSelect((int)CONTROL_TYPE.HUMAN);
        else
            Cmd_UpdateSelect((int)CONTROL_TYPE.GHOST);
    }

    [Command]
    void Cmd_UpdateSelect(int SelectType)
    {
        SyncSelectType = SelectType;
    }

    [Command]
    void  Cmd_UpdateToReady(bool value )
    {
        if (value)
        {
            switch (SyncSelectType)
            {
                case (int)CONTROL_TYPE.GHOST:

                    if (!CheckCanSelectGhost()) return;

                    m_lobbyGameManager.CanSelectGhost = false;


                    break;
                case (int)CONTROL_TYPE.HUMAN:

                    if (!CheckCanSelectHuman()) return ;


                    m_lobbyGameManager.m_SelectHuman++;

                    break;
            }

            SyncToRedy = value;
        }
        else {
            switch (SyncSelectType)
            {
                case (int)CONTROL_TYPE.GHOST:


                    m_lobbyGameManager.CanSelectGhost = true;
      
                    break;
                case (int)CONTROL_TYPE.HUMAN:

                    m_lobbyGameManager.m_SelectHuman--;
 
                    break;
            }

            SyncToRedy = value;
      
        }

    }
    
    bool CheckCanSelectGhost()
    {
        if (m_lobbyGameManager.CanSelectGhost == true)
        {
            return true;
        }
        return false;
    }

    bool CheckCanSelectHuman()
    {

        if (m_lobbyGameManager.m_SelectHuman < 3)
        {
            return true;
        }
        return false;
    }


    
}
