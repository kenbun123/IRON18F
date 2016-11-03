//using UnityEngine;
//using System.Collections;
//using UnityEngine.Networking;
//using System.Collections.Generic;
///// <summary>
///// CGhostMain.cs
/////  * \details CGhostMain	鬼のメイン関数
/////  * \author  Kenbun Kimoto
/////  * \date    ファイルの作成日	新規作成
/////</summary>
//public class GhostMain : MonoBehaviour
//{

//    //鬼の視覚状態
//    public enum GHOTS_VIEW_STATUS{
//        VISIBLE,
//        INVISIBLE
//    };

//    //鬼のステータス
//    public enum GHOTS_STATUS
//    {
//        NORMAL,
//        DASH,
//        STAN
//    }
//    public SGhostScore GhostScore;

//    public Animator m_animator;

//    public CStateMachine<GhostMain> m_pStateMachine;

//    public GHOTS_VIEW_STATUS m_viewStatus;

//    public GHOTS_STATUS m_Status;

//    public int m_nowDunmmyNum;

//    public GameObject atkParticle;


//    public GameObject m_nowAttackHuman;

//    public bool IsDebug;

//    [SerializeField]
//    private GameObject m_ui;

//    private GameObject UIObj;

//    private GameObject m_eatpatincle;
//    public GameObject Eatpatincle
//    {
//        get { return m_eatpatincle; }
//        set { m_eatpatincle = value; }
//    }


//    private GameObject m_movepatincle;
//    public GameObject Movepatincle

//    {
//        get { return m_movepatincle; }
//        set { m_movepatincle = value; }
//    }
//    //ノーマルスピード
//    [SerializeField]
//    private float m_normalSpeed;

//    public float NomalSpeed {
//        get { return m_normalSpeed; }
//    }

//    //スロー状態スピード
//    [SerializeField]
//    private float m_slowSpeed;
//    public float SlowSpeed
//    {
//        get { return m_slowSpeed; }
//    }

//    //ダッシュ中スピード
//    [SerializeField]
//    private float m_dashSpeed;
//    public float DashSpeed
//    {
//        get { return m_dashSpeed; }
//    }

//    //攻撃中スピード
//    [SerializeField]
//    private float m_atkSpeed;
//    public float AtkSpeed
//    {
//        get { return m_atkSpeed; }
//    }

//    [SerializeField]
//    private float m_showBuff;

//    public float ShowBuff
//    {
//        get { return m_showBuff; }
//        set { m_showBuff = value; }
//    }

//    private Vector3 m_moveDirection;

//    public Vector3 MoveDirection {
//        get { return m_moveDirection; }
//        set { m_moveDirection = value; }
//    }


//    private bool m_isAttack;
//    public bool IsAttack
//    {
//        get { return m_isAttack; }
//        set { m_isAttack = value; }
//    }

//    //スタン時間
//    public float m_stanInterval;

//    //最大Dummy数
//    public int m_maxDunmmyNum;


//    private bool m_isLocalPlayer;
//    public bool IsLocalPlayer{
//        get { return m_isLocalPlayer; }
//        set { m_isLocalPlayer = value; }
//    }

//    public void SetScore()
//    {
//        CGameScore.Instance.SetGhostScore(this);
//    }

//    private float m_nowTime;

//    public GameObject []dGhost;


//    CStage m_stage;
//    bool IsSet;
//    // Use this for initialization
//    void Start () {
//        m_showBuff = 1;
//        m_viewStatus = GHOTS_VIEW_STATUS.INVISIBLE;
//        m_pStateMachine = new CStateMachine<GhostMain>(this);
//        m_animator = this.GetComponent<Animator>();
//        m_pStateMachine.SetCurrentState(CGhostState_Main.Instance());
//        m_pStateMachine.SetGlobalStateState(CGhostState_Wait.Instance());
//        m_nowDunmmyNum = m_maxDunmmyNum;
//        dGhost = new GameObject[3];
//        GhostScore = new SGhostScore();
//        GhostScore.hitCandy = 0;
//        GhostScore.knockOut = 0;
//        int count = 1;
//        foreach (Transform i in this.transform)
//        {
//            if (i.name == ("ghostkodomo_" + count.ToString()))
//            {
//                dGhost[count - 1] = i.gameObject;
//                count++;
//            }
//            if(i.name=="GhostMeshParticle")
//            {
//                m_movepatincle = i.gameObject;
//            }
//            if (i.name == "CandyEatParticle")
//            {
//                m_eatpatincle = i.gameObject;
//            }
//        }
//        m_eatpatincle.SetActive(false);

//        IsSet = false;
//       // transform.position = m_stage.m_Pos[4];
//	}
	
//	// Update is called once per frame
//	void Update () {
//        if (m_stage == null)
//        {
//            GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("StageBase");
//            if (tagobjs.Length == 0)
//                return;

//            if (tagobjs[0] != null)
//            {
//                m_stage = tagobjs[0].GetComponent<CStage>();
//            }

//        }
//        else {
//            if (m_stage.m_Pos[4] != null && IsSet == false)
//            {
//                IsSet = true;
//                transform.position = new Vector3 (m_stage.m_Pos[3].x, m_stage.m_Pos[3].y, m_stage.m_Pos[3].z);
//            }

//        }



//        DrawUI();
//        if ( m_viewStatus == GHOTS_VIEW_STATUS.VISIBLE)
//        {
//            this.gameObject.layer = 0;
//            if (m_movepatincle!=null)
//            {
//                m_movepatincle.gameObject.SetActive(true);
//            }
           
//        }
//        else
//        {
//            this.gameObject.layer = 8;
//            if (m_movepatincle != null)
//            {
//                m_movepatincle.gameObject.SetActive(false);
//            }
//        }
//        m_pStateMachine.SMUpdate();
//	}

//    //トリガー
//    void OnCollisionTrigger(Collision other)
//    {
//        m_Status = GHOTS_STATUS.STAN;
//    }

//    //void OnTriggerStay(Collider other)
//    //{
//    //    if (other.gameObject.tag == "Candy")
//    //    {
//    //        if (other.GetComponent<CCandy>().m_isShot==true)
//    //        {
//    //            m_Status = GHOTS_STATUS.STAN;
//    //        }
//    //    }


//    //}

//	public void HitCandy() {

//        m_pStateMachine.ChangeState(CGhostState_Stan.Instance());
//        //CSoundManager.Instance.PlaySE(EAudioList.SE_DummyGhost);
        
//	}


//    public bool IsMotionEnd(string name)
//    {
//        bool isname = this.m_animator.GetCurrentAnimatorStateInfo(0).IsName(name);
//        bool istime = this.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;

//        if (istime)
//        {
//            return true;
//        }

//        return false;
//    }

//    public void DrawUI()
//    {
//        if (IsLocalPlayer || IsDebug)
//        {
//            if (UIObj == null)
//            {
//                UIObj = (GameObject)Instantiate(m_ui);
//            }
//            bool IsFull = false;
          
//            for (int i = 0; i < 3; i++)
//            {
//                if (dGhost[i].GetComponent<DummyGhost>().IsAct==true)
//                {
//                    UIObj.GetComponent<CGhostUI>().SetDMGhost(i);
//                }
//                else
//                {
//                    UIObj.GetComponent<CGhostUI>().HaveDMGhost(i);
//                }
//            }


//            if (m_viewStatus == GHOTS_VIEW_STATUS.VISIBLE)
//            {
//                UIObj.GetComponent<CGhostUI>().GhostToOn();
//            }
//            else
//            {
//                UIObj.GetComponent<CGhostUI>().GhostToOff();
//            }
//        }
//    }

//    public CStateMachine<GhostMain> GetFSM()
//    {
//        return m_pStateMachine;
//    }
//}
