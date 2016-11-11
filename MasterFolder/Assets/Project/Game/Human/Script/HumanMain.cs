using UnityEngine;
using System.Collections;

public class HumanMain : HumanInfo
{

    private FiniteStateMachine<HumanMain, HumanMain.HumanFiniteStatus> humanStateMachine;
    private Animator humanAnimation;
    private int candleStock;
    private int candyStock;
    private float stamina;

    public HumanFiniteStatus HumanStatusMessage;
    public bool CanChangeStatus;
    public Vector3 MoveDirection;
    public ParticleSystem impatienceParticle;
    public bool isFear;
    //public GameObject CarryHuman;

    #region get,set
    public float NormalSpeed
    {
        get { return normalSpeed; }
    }
    public float DashSpeed
    {
        get { return dashSpeed; }
    }
    public float Stamina {
        get { return stamina; }
        set { stamina = value; }
    }
    public float MaxStamina {
        get { return maxStamina; }
    }
    public float RaiseTime {
        get { return raiseTime; }
    }
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public int CandleStock {
        get { return candleStock; }
        set { candleStock = value; }
    }
    public int MaxCandleStock {
        get { return maxCandleStock; }
    }
    public int CandyStock {
        get { return candyStock; }
        set { candyStock = value; }
    }
    public int MaxCandyStock {
        get { return maxCandyStock; }
    }
    public Animator HumanAnimation
    {
        get { return humanAnimation; }
    }
    public FiniteStateMachine<HumanMain, HumanMain.HumanFiniteStatus> HumanStateMachine {
        get { return humanStateMachine; }
    }

    #endregion

    #region Method
    void Awake()
    {
        CanChangeStatus = true;
        stamina = maxStamina;
        HumanStatusMessage = HumanFiniteStatus.WAITING;
        isFear = false;
    }

    // Use this for initialization
    void Start()
    {
        //コンポーネントの取得
        humanAnimation = CheckComponentNull<Animator>.CheckConmponentNull(gameObject, "Class HumanMain : Don't Get Animator Component");
        InitState();

        //汗パーティクル
        var tmpPrefab = (GameObject)Resources.Load("Prefab/ImpatienceParticle");
        var tmpGameobj = Instantiate(tmpPrefab);
        tmpGameobj.transform.parent = transform;
        tmpGameobj.transform.position = transform.position;
        tmpGameobj.transform.SetY(1.0f);
        tmpGameobj.transform.localScale = new Vector3(1, 1, 1);
        impatienceParticle = CheckComponentNull<ParticleSystem>.CheckConmponentNull(tmpGameobj, "Class HumanMain : Don't Get ParticleSystem Component");

    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            HumanStatusMessage = HumanFiniteStatus.DEATH;
        }

        humanStateMachine.Update();

        if (stamina <= 0)
        {
            if (!impatienceParticle.isPlaying)
            {
                impatienceParticle.Play();
            }
        }
        else {
            impatienceParticle.Stop();
        }
    }
    public void Damage(int atk)
    {
        hp -= atk;
        if (hp < 0) hp = 0;  //マイナス回避
    }

    void InitState()
    {

        //ステート定義
        humanStateMachine = new FiniteStateMachine<HumanMain, HumanFiniteStatus>(this);
        humanStateMachine.ChangeState(humanStateMachine.RegisterState(new HumanState.HumanStatusWaiting()));
        humanStateMachine.RegisterState(new HumanState.HumanStatusWalk());
        humanStateMachine.RegisterState(new HumanState.HumanStatusDash());
        humanStateMachine.RegisterState(new HumanState.HumanStatusPickUpCandle());
        humanStateMachine.RegisterState(new HumanState.HumanStatusPutCandle());
        humanStateMachine.RegisterState(new HumanState.HumanStatusPickUpCandy());
        humanStateMachine.RegisterState(new HumanState.HumanStatusUseCandy());
        humanStateMachine.RegisterState(new HumanState.HumanStatusDeath());
        humanStateMachine.RegisterState(new HumanState.HumanStatusAction());

    }
    #endregion
}
