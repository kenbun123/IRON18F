using UnityEngine;
using System.Collections;

public class HumanMain : HumanInfo
{

    private FiniteStateMachine<HumanMain, HumanMain.HumanFiniteStatus> humanStateMachine;
    private Animator humanAnimation;
    private int candleStock;


    public HumanFiniteStatus HumanStatusMessage;
    public bool CanChangeStatus;
    [HideInInspector]
    public Vector3 MoveDirection;



    public float NormalSpeed
    {
        get { return normalSpeed; }
    }
    public float DashSpeed
    {
        get { return dashSpeed; }
    }
    public int Hp
    {
        get { return hp; }
    }
    public Animator HumanAnimation {
        get { return humanAnimation; }
    }
    public int CandleStock {
        get { return candleStock; }
        set { candleStock = value; }
    }
    public int MaxCandleStock {
        get { return maxCandleStock; }
    }

    void Awake()
    {
        CanChangeStatus = true;
    }
    // Use this for initialization
    void Start()
    {

        HumanStatusMessage = HumanFiniteStatus.WAITING;

        //コンポーネントの取得
        humanAnimation = CheckComponentNull<Animator>.CheckConmponentNull(this, "Class HumanMain : Don't Get Animator Component");

        //ステート定義
        humanStateMachine = new FiniteStateMachine<HumanMain, HumanFiniteStatus>(this);
        humanStateMachine.ChangeState(humanStateMachine.RegisterState(new HumanState.HumanStatusWaiting()));
        humanStateMachine.RegisterState(new HumanState.HumanStatusWalk());
        humanStateMachine.RegisterState(new HumanState.HumanStatusDash());
        humanStateMachine.RegisterState(new HumanState.HumanStatusPickUpCandle());
        humanStateMachine.RegisterState(new HumanState.HumanStatusPutCandle());

    }

    // Update is called once per frame
    void Update()
    {
        humanStateMachine.Update();

    }
    void FixedUpdate()
    {
        
    }
    public void Damage(int atk)
    {
        hp -= atk;
        if (hp < 0) hp = 0;  //マイナス回避
    }


}
