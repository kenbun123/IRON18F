using UnityEngine;
using System.Collections;

public class CHuman : MonoBehaviour
{

    private GameObject m_carryHuman;//運ぶ用容器
    private GameObject m_find;
    private GameObject m_candy;
    private GameObject m_ghost;
    private GameObject[] m_dummyGhost;
    private GameObject m_candle;
    private CHuman[] m_humanList;
    private CStateMachine<CHuman> m_pStateMachine;//定义一个状态机  
    private Vector3 m_moveDirection;
    private Animator m_animator;

    private SHumanScore m_shumanScore;

    [SerializeField]
    private GameObject m_ui;

    private GameObject UIObj;

    [SerializeField]
    private float m_moveSpeed;
    public float DashSpeed;
    private int m_hunmanTime;
    [SerializeField]
    private float strength;
    private float m_hunmanstrength;
    [SerializeField]
    private int m_hp;
    [SerializeField]
    private bool m_dead;
    private bool m_itemFlag;
    private bool m_candyFlag;
    private bool m_carryFlag;
    [SerializeField]
    private bool m_lightOn;
    private int m_carryMove;
    [SerializeField]
    private int MaxLifeTime;
    private int m_reLife;
    private bool m_isReLifeScoreOn;
    // [SerializeField]
    public int m_humanID = 0;
    private bool IsColorSetOn;
    public bool IsLocal;
    public Material[] HumanMaterial;
    private ParticleSystem m_exMark;
    private ParticleSystem m_impatiencle;
    private ParticleSystem m_chikin;
    private int HumanCount = 0;

    public float Area;
    public bool IsDebug;
    public CBeat beatSE;


    bool m_IsSetPos;
    /// <summary>
    /// アクセス
    /// </summary>
    /// 
    CStage m_stage;

    public int HP {
    get { return m_hp; }
    }
    public GameObject UI
    {
        get { return m_ui; }
        set { m_ui = value; }
    }

    public GameObject[] DummyGhost
    {
        get { return m_dummyGhost; }
    }

    public Animator AnimatorEDT
    {
        get { return m_animator; }
        set { m_animator = value; }
    }

    public CStateMachine<CHuman> PStateMachine
    {
        get { return m_pStateMachine; }
        set { m_pStateMachine = value; }
    }

    public GameObject CarryHuman
    {
        get { return m_carryHuman; }
        set { m_carryHuman = value; }
    }

    public ParticleSystem ExMark
    {
        get { return m_exMark; }
        set { m_exMark = value; }
    }

    public ParticleSystem Impatiencle
    {
        get { return m_impatiencle; }
        set { m_impatiencle = value; }
    }

    public ParticleSystem Chikin
    {
        get { return m_chikin; }
        set { m_chikin = value; }
    }

    public GameObject Candle
    {
        get { return m_candle; }
        set { m_candle = value; }
    }

    public GameObject Candy
    {
        get { return m_candy; }
        set { m_candy = value; }
    }

    public GameObject Find
    {
        get { return m_find; }
        set { m_find = value; }
    }

    public GameObject Ghost
    {
        get { return m_ghost; }
        set { m_ghost = value; }
    }

    public SHumanScore ShumanScore
    {
        get { return m_shumanScore; }
        set { m_shumanScore = value; }
    }

    public Vector3 MoveDirection
    {
        get { return m_moveDirection; }
        set { m_moveDirection = value; }
    }


    public float MoveSpeed
    {
        get { return m_moveSpeed; }
        set { m_moveSpeed = value; }
    }

    public int HunmanTime
    {
        get { return m_hunmanTime; }
        set { m_hunmanTime = value; }
    }
    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    public float HunmanStrength
    {
        get { return m_hunmanstrength; }
        set { m_hunmanstrength = value; }
    }

    public bool Dead
    {
        get { return m_dead; }
        set { m_dead = value; }
    }

    public bool ItemFlag
    {
        get { return m_itemFlag; }
        set { m_itemFlag = value; }
    }

    public bool CandyFlag
    {
        get { return m_candyFlag; }
        set { m_candyFlag = value; }
    }


    public bool CarryFlag
    {
        get { return m_carryFlag; }
        set { m_carryFlag = value; }
    }

    public bool LightOn
    {
        get { return m_lightOn; }
        set { m_lightOn = value; }
    }

    public bool IsReLifeScoreOn
    {
        get { return m_isReLifeScoreOn; }
        set { m_isReLifeScoreOn = value; }
    }

    public int CarryMove
    {
        get { return m_carryMove; }
        set { m_carryMove = value; }
    }

    public int HumanID
    {
        get { return m_humanID; }
        set { m_humanID = value; }
    }

    public int ReLife
    {
        get { return m_reLife; }
        set { m_reLife = value; }
    }

    /// <summary>
    /// 設定
    /// </summary>
    public void StopHumanTime()
    {
        m_hunmanTime = 0;
    }
    public void StartHumanTime()
    {
        m_hunmanTime = 1;
    }
    /// <summary>
    /// 動作
    /// </summary>
    public void PlusStrength(float v)
    {

        m_hunmanstrength = v;


    }
    public void MinusStrength(float v)
    {
        if (m_hunmanstrength > 0)
        {
            m_hunmanstrength -= v;
        }

    }
    public void Damage()
    {
        m_hp = 0;
    }
    public bool Doa()
    {
        if (m_hp < 1)
        {
            if (m_reLife == MaxLifeTime)
            {
                m_reLife = 0;
            }
            return true;
        }
        return false;
    }
    public void ReLifeOn()
    {

        if (m_reLife < MaxLifeTime)
        {
            m_reLife++;/////////////////////////////////////
            if (m_reLife == MaxLifeTime)
            {
                m_hp = 1;
                m_pStateMachine.ChangeState(CHumanState_Main.Instance());
            }
        }
    }

    public void SetID(int x)
    {
        this.m_humanID = x;
    }

    public void SetScore()
    {
        CGameScore.Instance.SetHumanScore(this);
    }
    /// <summary>
    /// Main
    /// </summary>
    ///z

    void Start()
    {
        IsLocal = false;
        m_humanList = new CHuman[3];
        if (m_dummyGhost == null)
        {
            m_dummyGhost = GameObject.FindGameObjectsWithTag("DummyGhost");//優先
        }

        if (m_ghost == null)
        {
            m_ghost = GameObject.Find("Ghost(Clone)");//優先
            if (m_ghost == null)
            {
                m_ghost = GameObject.Find("Ghost");
            }
        }
        if (m_find == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "find")
                {
                    m_find = child.gameObject;
                }
            }
        }
        if (m_candy == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "Candy")
                {
                    m_candy = child.gameObject;
                }
            }
        }
        if (m_animator == null)
        {
            m_animator = this.GetComponent<Animator>();
        }

        if (ExMark == null || Impatiencle == null || Chikin == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "ExclamationMark")
                {
                    ExMark = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "ImpatienceParticle")
                {
                    Impatiencle = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "StanChickParticle")
                {
                    Chikin = child.GetComponent<ParticleSystem>();
                    Chikin.gameObject.SetActive(false);
                }
            }

        }

        beatSE = new CBeat();
        m_shumanScore = new SHumanScore();
        m_hp = 1;
        m_reLife = MaxLifeTime;
        m_hunmanstrength = strength;
        m_hunmanTime = 1;
        m_pStateMachine = new CStateMachine<CHuman>(this);//初始化状态机  
        m_pStateMachine.SetCurrentState(CHumanState_Main.Instance()); //设置一个当前状态  
        m_pStateMachine.SetGlobalStateState(CHumanState_Wait.Instance());//设置全局状态  
        m_candyFlag = true;
        ShumanScore.resuscitation = 0;
        ShumanScore.setAlterCandle = 0;
        ShumanScore.setCandle = 0;
        m_isReLifeScoreOn = false;



        m_IsSetPos = false;
    }

    void Update()
    {
        if (m_humanID == 0)
        {
            GameObject obj = GameObject.Find("UI");
            m_humanID = obj.GetComponent<CY>().ID;
            obj.GetComponent<CY>().ID++;
        }

        //if (m_IsSetPos == false)
        //{
        //    GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("StageBase");

        //    if (tagobjs[0] != null)
        //    {
        //        m_stage = tagobjs[0].GetComponent<CStage>();
        //    }
        //    if (IsLocal&&m_stage!= null)
        //    {
        //        transform.position = new Vector3(m_stage.m_Pos[m_humanID - 1].x, m_stage.m_Pos[m_humanID - 1].y, m_stage.m_Pos[m_humanID - 1].z);
        //        m_IsSetPos = true;
        //    }
        //}
        ColorChange();
        DrawUI();
        if (m_dummyGhost == null || m_dummyGhost.Length < 3)
        {
            m_dummyGhost = GameObject.FindGameObjectsWithTag("DummyGhost");//優先
        }
        if (m_ghost == null)
        {
            m_ghost = GameObject.Find("Ghost(Clone)");//優先
            if (m_ghost == null)
            {
                m_ghost = GameObject.Find("Ghost");
            }
        }
        if (m_find == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "find")
                {
                    m_find = child.gameObject;
                }
            }
        }

        if (m_candy == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "Candy")
                {
                    m_candy = child.gameObject;
                }
            }
        }

        if (m_animator == null)
        {
            m_animator = this.GetComponent<Animator>();
        }

        if (ExMark == null || Impatiencle == null || Chikin == null)
        {
            foreach (Transform child in this.transform)
            {
                if (child.name == "ExclamationMark")
                {
                    ExMark = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "ImpatienceParticle")
                {
                    Impatiencle = child.GetComponent<ParticleSystem>();
                }
                if (child.name == "StanChickParticle")
                {
                    Chikin = child.GetComponent<ParticleSystem>();
                    Chikin.gameObject.SetActive(false);
                }
            }
        }

        if (Doa())
        {
            m_pStateMachine.ChangeState(CHumanState_Dead.Instance());
        }

        if (HunmanStrength <= 0)
        {
            if (!Impatiencle.isPlaying)
            {
                Impatiencle.Play();
            }

        }
        else
        {
            Impatiencle.Stop();
            //  Impatience.Clear();
        }
    }

    public void DrawUI()
    {
        if (IsLocal || IsDebug)
        {
            if (UIObj == null)
            {
                UIObj = (GameObject)Instantiate(UI);
                return;////////////////////////////////////////////////////////////////////////////////////////////これを追加した
            }
            if (HumanCount < 3)
            {
                GameObject[] tmpObj = GameObject.FindGameObjectsWithTag("Human");
                HumanCount = tmpObj.Length;
                for (int i = 0; i < HumanCount; i++)
                {
                    if (tmpObj[i] != null)
                    {
                        for (int j = 0; j < tmpObj.Length; j++)
                        {
                            if (i == tmpObj[j].GetComponent<CHuman>().m_humanID - 1)
                            {
                                m_humanList[i] = tmpObj[j].GetComponent<CHuman>();
                            }
                        }

                    }

                }
            }
            for (int i = 0; i < HumanCount; i++)
            {
                if (m_humanList[i].Doa())
                {
                    UIObj.GetComponent<CHumanUI>().Dead(i);
                }
                else
                {
                    UIObj.GetComponent<CHumanUI>().Alive(i);
                }

                if (m_candyFlag)
                {
                    UIObj.GetComponent<CHumanUI>().HaveCandy(i);
                }
                else
                {
                    UIObj.GetComponent<CHumanUI>().ThrowCandy(i);
                }
            }


            if (m_itemFlag)
            {
                UIObj.GetComponent<CHumanUI>().HaveCandle();
            }
            else
            {
                UIObj.GetComponent<CHumanUI>().PutCandle();
            }
        }
    }

    public void ColorChange()
    {
        if (this.IsColorSetOn == false)
        {
            GameObject tmp_obj = null;
            foreach (Transform child in this.transform)
            {
                if (child.name == "polySurface12")
                {
                    tmp_obj = child.gameObject;
                }
            }
            if (tmp_obj != null)
            {
                switch (this.m_humanID)
                {
                    case 0:
                        ChildSet(tmp_obj.transform, HumanMaterial[0]);
                        break;
                    case 1:
                        ChildSet(tmp_obj.transform, HumanMaterial[0]);
                        break;
                    case 2:
                        ChildSet(tmp_obj.transform, HumanMaterial[1]);
                        break;
                    case 3:
                        ChildSet(tmp_obj.transform, HumanMaterial[2]);
                        break;
                }
            }

            this.IsColorSetOn = true;
        }

    }

    public void ChildSet(Transform tr, Material mat)
    {
        foreach (Transform child in tr)
        {
            if (child.name == "polySurface12")
            {
                foreach (Transform tmp in child)
                {
                    tmp.GetComponent<SkinnedMeshRenderer>().material = mat;
                }
            }

        }
    }

    public bool IsMotionEnd(string name)
    {
        bool isname = this.AnimatorEDT.GetCurrentAnimatorStateInfo(0).IsName(name);
        bool istime = this.AnimatorEDT.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;

        if (istime)
        {
            return true;
        }

        return false;
    }

    public CStateMachine<CHuman> GetFSM()
    {
        return m_pStateMachine;
    }
}
