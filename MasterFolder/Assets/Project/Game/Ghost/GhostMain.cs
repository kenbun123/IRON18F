using UnityEngine;
using System.Collections;

public class GhostMain : GhostInfo {

    [SerializeField]
    private bool canView;
    [SerializeField]
    private bool isLocalPlayer;
    private Vector3 direction;
    private Animator ghostAnimation;
    private FiniteStateMachine<GhostMain, GhostInfo.GhostFiniteStatus> ghostStateMachine;
    

    public GhostFiniteStatus GhostStatusMessage;
    public bool CanChangeStatus;
    public bool CanView
    {
        get { return canView; }
        set { canView = value; }
    }
    public bool IsLocalPlayer {
        get { return isLocalPlayer; }
        set { isLocalPlayer = value; }
    }
    public CapsuleCollider AttackCollider;
    public Animator GhostAnimation {
        get { return ghostAnimation; }
    }
    public Vector3 Direction {
        get { return direction; }
        set { direction = value; }
    }

    public FiniteStateMachine<GhostMain, GhostInfo.GhostFiniteStatus> GhostStateMachine {
        get { return ghostStateMachine; }
    }

    void Awake()
    {
        canView = false;
        CanChangeStatus = true;
        GhostStatusMessage = GhostFiniteStatus.WAITING;
    }

    void Start()
    {

        ghostAnimation = GetComponent<Animator>();
        InitState();

    }


    void Update()
    {
        ghostStateMachine.Update();

    }


    void InitState()
    {
        ghostStateMachine = new FiniteStateMachine<GhostMain, GhostFiniteStatus>(this);
        ghostStateMachine.ChangeState(ghostStateMachine.RegisterState(new GhostState.GhostStatusWait()));
        ghostStateMachine.RegisterState(new GhostState.GhostStatusDash());
        ghostStateMachine.RegisterState(new GhostState.GhostStatusWalk());
        ghostStateMachine.RegisterState(new GhostState.GhostStatusSlow());
        ghostStateMachine.RegisterState(new GhostState.GhostStatusAttack());
        ghostStateMachine.RegisterState(new GhostState.GhostStatusStan());
        ghostStateMachine.RegisterState(new GhostState.GhostStatusDummy());

    }
}
