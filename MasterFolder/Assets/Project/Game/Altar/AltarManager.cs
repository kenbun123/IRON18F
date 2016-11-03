using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class AltarManager : NetworkBehaviour {

    public int m_maxAltarNum;

	public int m_winAltarNum;

    public int m_nowLightAltar;


    
	// Use this for initialization
	void Start (){
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!isServer) return;

        m_nowLightAltar = 0;
        m_maxAltarNum = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            m_maxAltarNum++;

            if (transform.GetChild(i).GetComponent<Altar>().isLight)
            {
                m_nowLightAltar++;
            }
        }
        //全部灯せばゲームクリア
        if ( m_winAltarNum <= m_nowLightAltar )
        {
            //追加
            //ゲーム終了処理
            //GameObject.Find("EndMgr").GetComponent<CGameEnd>().isHumanWin = true;
        }

	}
}
