////!  CGameEnd.cs
///*!
// * \details 	CGameEnd
// * \author  show kondou
// * \date    2016 10 / 28	新規作成
// */
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking;



////!  CGameEnd.cs
///*!
// * \details CGameEnd	説明
// * \author  show Kondu
// * \date    2016 10 / 28	新規作成
// */
//public class CGameEnd : NetworkBehaviour {
//	// ===== 定数 =====
//	private const string TAG_HUMAN = "Human";		// 人のタグ
//	private const string ALTAR_MGR = "AltarManager";	// アルターマネージャー



//	// ===== メンバ変数 =====
//	private List<CHuman>    m_humanList = new List<CHuman>();    // 人のインスタンス
//	private AltarManager    m_altarMgr;     // 祭壇Mgrインスタン
//	private int             m_HumanNum;     // 人の数
//	public bool m_isInit = false;			// 初期化判定

//    [SyncVar]
//    public bool isGhostWin = false;

//    [SyncVar]
//    public bool isHumanWin = false;


//    // ===== メンバ関数 =====
//    /*!  Start()
//	*!   \details	オブジェクト生成時呼び出し関数
//	*!
//	*!   \return	none
//	*/
//    void Start () {
//		Init();
//	}



//	/*!  Update()
//	*!   \details	更新時呼び出し関数
//	*!
//	*!   \return	none
//	*/
//	void Update () {
//        if (isServer)
//        {
//            Init();
//            CheckHuman();
//        }



//        if (isGhostWin)
//        {
//            CGameScore.Instance.WinGhost();
//            // ゴースト勝利イベント
//            WinGhost();

//        }

//        if (isHumanWin)
//        {
//            CGameScore.Instance.WinHuman();
//            WinHuman();

//        }
//    }



//	/*!  Init()
//	*!   \details	初期化
//	*!
//	*!   \return	none
//	*/
//	private void Init() {
//		// インスタン所得
//		GameObject[] humans = GameObject.FindGameObjectsWithTag( TAG_HUMAN );
//		GameObject altarMgr = GameObject.Find( ALTAR_MGR );

//		// インスタンス保存
//		for( int i = 0; i < humans.Length; i ++ ) {
//			m_humanList.Add( humans[i].GetComponent<CHuman>() );
//		}
//		m_altarMgr = altarMgr.GetComponent<AltarManager>();

//		m_HumanNum = m_humanList.Count;

//		// 取得ジェック
//		// Debug.Log( "人の数：" + m_HumanNum );
//		// Debug.Log( "祭壇：" + m_altarMgr );
//		m_isInit = 0 < m_humanList.Count;
//	}



//	/*!  CheckHuman()
//	*!   \details	人のチェック
//	*!
//	*!   \return	none
//	*/
//	private void CheckHuman() {
        
//		if( !m_isInit )
//			return;
//		int deadNum = 0;
//		foreach( var i in m_humanList ) {
//			bool isDead = i.Doa();
//			if( isDead ) {
//				deadNum ++;
//			}
//		}
//		//
//		isGhostWin = m_HumanNum == deadNum;






//	}



//	/*!  WinHuman()
//	*!   \details	人のチェック
//	*!
//	*!   \return	none
//	*/
//	public void WinHuman() {
//		Debug.Log( "人の勝ち" );
//        if (isServer)
//            NetworkManager.singleton.ServerChangeScene("Result");
//       // FadeManager.Instance.LoadLevel( SCENE_RAVEL.RESULT, 0.5F, null, NetworkManager.singleton.ServerChangeScene );// .LoadLevel( SCENE_RAVEL.RESULT, 0.5F, null );

//    }



//	/*!  WinHuman()
//	*!   \details	人のチェック
//	*!
//	*!   \return	none
//	*/
//	public void WinGhost() {
//		Debug.Log( "お化けの勝ち" );
//        if (isServer)
//            NetworkManager.singleton.ServerChangeScene("Result");


//	}


//}
