using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CLobbyPlayer : NetworkBehaviour
{





    [SyncVar]
    public int m_SyncId;


    private int m_id;


    public int Id {
        get { return m_id; }
    }

    public GameObject m_myCursor;


    private CLobbySelect m_lobbySelect;

    private CLobbyGameManager m_lobbyGame;

    // Use this for initialization
    void Start()
    {

        m_lobbyGame = GameObject.Find("LobbyGameManager").GetComponent<CLobbyGameManager>();

        if (isServer)
        {
            m_SyncId = m_lobbyGame.m_NowConnect;
        }


        m_lobbySelect = gameObject.GetComponent<CLobbySelect>();

        List<GameObject> list = GetAllChildren.GetAll(gameObject);

        if (isLocalPlayer)
        {
            foreach (GameObject obj in list)
            {
                if (obj.name == "localCursor")
                {

                    obj.SetActive(true);
                    m_myCursor = obj;

                }
            }
        }
        else {

            foreach (GameObject obj in list)
            {
                if (obj.name == "anotherCursor")
                {

                    obj.SetActive(true);
                    m_myCursor = obj;


                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (m_SyncId != 0)
        {
            if (m_myCursor.GetComponent<CMatchingCursor>().m_id != 999 && m_myCursor.GetComponent<CMatchingCursor>().IsSetId == false)
            {
                m_myCursor.GetComponent<CMatchingCursor>().SetId((m_SyncId - 1));
            }
        }

        if (m_lobbySelect.SyncSelectType == (int)CLobbySelect.CONTROL_TYPE.GHOST)
        {
            m_myCursor.GetComponent<CMatchingCursor>().m_isGhostSelect = true;
        }
        if (m_lobbySelect.SyncSelectType == (int)CLobbySelect.CONTROL_TYPE.HUMAN)
        {
            m_myCursor.GetComponent<CMatchingCursor>().m_isGhostSelect = false;
        }

    }


    void OnGUI() {
        if (isLocalPlayer && gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin)
        {
            GUI.Label(new Rect(100, 0, 100, 30), "Redy");
        }
    

    }
}
