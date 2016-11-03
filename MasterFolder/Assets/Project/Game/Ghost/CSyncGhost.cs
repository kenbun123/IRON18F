//using UnityEngine;
//using System.Collections;
//using UnityEngine.Networking;
//using System.Collections.Generic;

//public class CSyncGhost : NetworkBehaviour
//{

//    GhostMain m_ghostMain;


//    [SyncVar]
//    public Vector3 m_SyncPostion;

//    [SyncVar]
//    public Quaternion m_SyncRotation;

//    //通信
//    [SyncVar]
//    public int m_status;

//    [SyncVar]
//    public int m_globalStatus;

//    [SyncVar]
//    public GameObject m_SyncAttackHuman;

//    //補間率
//    private float lerpRate = 10;

//    private float threshold = 0.1f;


//    void Start()
//    {

//        if (transform.GetComponent<GhostMain>())
//        {
//            m_ghostMain = transform.GetComponent<GhostMain>();
//            m_ghostMain.IsLocalPlayer = isLocalPlayer;
//        }

//        if (isLocalPlayer)
//        {

//            transform.GetComponent<GhostControl>().enabled = true;
//            Cmd_UpdateTransform(transform.position, transform.rotation);
//            Cmd_SyncAtkHuman(m_ghostMain.m_nowAttackHuman);

//        }


//    }

//    void Update()
//    {
//        if (isLocalPlayer)
//        {
//            if (Quaternion.Angle(transform.rotation, m_SyncRotation) > threshold || Vector3.Distance(transform.position, m_SyncPostion) > threshold)
//            {
//                Cmd_UpdateTransform(transform.position, transform.rotation);
//            }

//            if (m_ghostMain.m_pStateMachine.CurrentState().ID != m_status ||
//                m_ghostMain.m_pStateMachine.GlobalState().ID != m_globalStatus)
//            {
//                Cmd_UpdateClient(m_ghostMain.m_pStateMachine.CurrentState().ID, m_ghostMain.m_pStateMachine.GlobalState().ID);
//            }

//            // Debug.Log("Command");

//            Cmd_SyncAtkHuman(m_ghostMain.m_nowAttackHuman);
            


//        }
//        else
//        {

//            UpdateServer();
//            m_ghostMain.m_nowAttackHuman = m_SyncAttackHuman;
//            //位置情報の補間
//            switch (m_status)
//            {
//                case (int)GhostStateID.MOVE:
//                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_ghostMain.NomalSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position) * 15.0f;
//                    break;
//                case (int)GhostStateID.DASH:
//                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_ghostMain.DashSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position) * 15.0f;
//                    break;
//                /*追加した*/
//                case (int)GhostStateID.ATK:
//                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_ghostMain.DashSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position) * 15.0f;
//                    break;


//            }

//            transform.rotation = m_SyncRotation;


//        }

//    }


//    [Command(channel = 2)]
//    void Cmd_UpdateClient(int status, int global)
//    {
//        m_status = status;
//        m_globalStatus = global;
//    }

//    [Command(channel = 1)]
//    void Cmd_UpdateTransform(Vector3 pos, Quaternion rota)
//    {
//        m_SyncPostion = pos;
//        m_SyncRotation = rota;
//    }

//    void UpdateServer()
//    {

//        switch (m_status)
//        {

//            case (int)GhostStateID.MAIN:

//                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Main.Instance());
//                break;
//            case (int)GhostStateID.ATK:

//                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Atk.Instance());
//                break;
//            case (int)GhostStateID.AVATAR:

//                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Avatar.Instance());
//                break;
//            case (int)GhostStateID.DASH:

//                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Dash.Instance());
//                break;
//            case (int)GhostStateID.MOVE:

//                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Move.Instance());
//                break;

//            case (int)GhostStateID.STAN:

//                m_ghostMain.m_pStateMachine.ChangeState(CGhostState_Stan.Instance());
//                break;


//        }

//        switch (m_globalStatus)
//        {
//            case (int)GhostStateID.WAIT:
//                m_ghostMain.m_pStateMachine.ChangeGlobalState(CGhostState_Wait.Instance());
//                break;
//            case (int)GhostStateID.SLOW:
//                m_ghostMain.m_pStateMachine.ChangeGlobalState(CGhostState_Slow.Instance());
//                break;

//        }
//    }

//    [Command(channel = 0)]
//    void Cmd_SyncAtkHuman(GameObject human)
//    {
//        Debug.Log("Sync_GameObject");
//        m_SyncAttackHuman = human;
//    }
//}
