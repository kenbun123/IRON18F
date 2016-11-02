using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CSpawnNetworkManager : MonoBehaviour {


    public GameObject m_Lobby;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (NetworkManager.singleton == null)
        {
          var value =   Instantiate(m_Lobby);
            value.name = "LobbyManager";
        }
	}
}
