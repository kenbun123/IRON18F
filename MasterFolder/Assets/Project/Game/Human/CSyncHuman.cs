using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class CSyncHuman : NetworkBehaviour {


    [SyncVar]
    public Vector3 m_SyncPostion;

    [SyncVar]
    public Quaternion m_SyncRotation;

    [SyncVar]
    public int m_synclocalHumanState;

    [SyncVar]
    public int m_syncGlobalHumanState;

    [SyncVar]
    public GameObject m_SyncCandle;

    CHuman m_human;

    //補間率
    private float lerpRate = 10;

    private float threshold = 0.1f;
    private float threshold_rotation = 10.0f;

    // Use this for initialization
    void Start() {
        m_human = gameObject.GetComponent<CHuman>();
        if (isLocalPlayer || isServer)
        {
            gameObject.GetComponent<CHumanControl>().enabled = true;
            m_human.IsLocal = true;

        }

        if (isLocalPlayer)
        {

            Cmd_SyncPosition(transform.position);
            Cmd_SyncRotaion(transform.rotation);
            Cmd_SyncLocalState((int)m_human.PStateMachine.CurrentState().ID);
            Cmd_SyncGlobalState((int)m_human.PStateMachine.GlobalState().ID);
        }
	}

    void Update()
    {

        if (!isLocalPlayer)
        {
            //位置情報の補間
            switch (m_synclocalHumanState)
            {
                case (int)StateID.MOVE:
                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_human.MoveSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position)*20.0f ;
                    break;
                case (int)StateID.DASH:
                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_human.DashSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position)*20.0f;
                    break;
                case (int)StateID.DEAD:
                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_human.MoveSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position) * 20.0f;
                    break;
                case (int)StateID.CARRY:
                    transform.position += Vector3.Normalize(m_SyncPostion - transform.position) * m_human.MoveSpeed * Time.deltaTime * Vector3.Magnitude(m_SyncPostion - transform.position) * 20.0f;
                    break;
            }

            if (Quaternion.Angle(transform.rotation, m_SyncRotation) > threshold_rotation)

            {
                //角度の補間
                transform.rotation = Quaternion.Lerp(transform.rotation, m_SyncRotation, Time.deltaTime * lerpRate);
            }

            if (m_human.Candle != m_SyncCandle)
            {
                m_human.Candle = m_SyncCandle;
                if (!isServer) return;
                
               if (m_human.Candle != null) m_human.Candle.GetComponent<CCandle>().IsStock = true;


                
                
            }

            SetState();
            
        }
        else
        {
            UpdateServer();
        }


    }



    [Client]
    void UpdateServer()
    {
        if (  Vector3.Distance(transform.position, m_SyncPostion) > threshold)
        {
            Cmd_SyncPosition(transform.position);
        }

        if (Quaternion.Angle(transform.rotation, m_SyncRotation) > threshold_rotation)
        {
            Cmd_SyncRotaion(transform.rotation);
        }


        if ( m_human.PStateMachine.CurrentState().ID != m_synclocalHumanState)
        {
            Cmd_SyncLocalState((int)m_human.PStateMachine.CurrentState().ID);
        }

        if (m_human.PStateMachine.GlobalState().ID != m_syncGlobalHumanState)
        {
            Cmd_SyncGlobalState((int)m_human.PStateMachine.GlobalState().ID);
        }

        if (m_SyncCandle != m_human.Candle)
        {
            Cmd_SyncCandle(m_human.Candle);
        }

        //Cmd_SyncTransform(transform.position, transform.rotation);
        // Cmd_SyncState((int)m_human.PStateMachine.GlobalState().ID, (int)m_human.PStateMachine.CurrentState().ID);
    }

    [Command(channel = 1)]
    void Cmd_SyncPosition(Vector3 pos)
    {
        m_SyncPostion = pos;
    }

    [Command(channel = 1)]
    void Cmd_SyncRotaion(Quaternion qua)
    {
        m_SyncRotation = qua;    
    }


    [Command(channel = 2)]
    void Cmd_SyncLocalState(int localId)
    {

        m_synclocalHumanState = localId;
    }

    [Command(channel = 2)]
    void Cmd_SyncGlobalState(int globalId)
    {
        m_syncGlobalHumanState = globalId;
    }
    //

    [Command(channel = 2)]
    void Cmd_SyncCandle(GameObject Candle)
    {

        m_SyncCandle = Candle;

    }

    void SetState()
    {

        switch (m_synclocalHumanState)
        {
            case (int)StateID.BIKURI:
                m_human.PStateMachine.ChangeState(CHumanState_Bikuri.Instance());
                break;
            case (int)StateID.CARRY:
                m_human.PStateMachine.ChangeState(CHumanState_Carry_Motion.Instance());
                break;
            case (int)StateID.DASH:
                m_human.PStateMachine.ChangeState(CHumanState_Dash_Motion.Instance());
                break;
            case (int)StateID.DEAD:
                m_human.PStateMachine.ChangeState(CHumanState_Dead.Instance());
                break;
            case (int)StateID.GET:
                m_human.PStateMachine.ChangeState(CHumanState_Get.Instance());
                break;
            case (int)StateID.ITEM:
                m_human.PStateMachine.ChangeState(CHumanState_Item.Instance());
                break;
            case (int)StateID.MAIN:
                m_human.PStateMachine.ChangeState(CHumanState_Main.Instance());
                break;
            case (int)StateID.MOVE:
                m_human.PStateMachine.ChangeState(CHumanState_Move_Motion.Instance());
                break;
            case (int)StateID.SET:
                m_human.PStateMachine.ChangeState(CHumanState_Set.Instance());
                break;
            case (int)StateID.USE:
                m_human.PStateMachine.ChangeState(CHumanState_Use.Instance());
                break;

        }

        switch (m_syncGlobalHumanState)
        {
            case (int)StateID.PANIK:
                m_human.PStateMachine.SetGlobalStateState(CHumanState_Perception.Instance());
                break;
            case (int)StateID.WAIT:
                m_human.PStateMachine.SetGlobalStateState(CHumanState_Wait.Instance());
                break;
        }
    }

}
