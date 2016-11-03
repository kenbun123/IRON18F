using UnityEngine;
using System.Collections;

public class HumanMain : HumanInfo
{


    public Animation HumanAnimatiom;

    private FiniteStateMachine<HumanMain, HumanMain.HumanFiniteStatus> humanStateMachine;
    public float MoveSpeed
    {
        get { return normalSpeed; }
    }
    public float DashSpped
    {
        get { return dashSpeed; }
    }
    public int Hp
    {
        get { return hp; }
    }

    
    void Awake()
    {
        humanStateMachine.ChangeState(new HumanState.HumanStatusWaiting());
    }
    // Use this for initialization
    void Start()
    {
        //gameObject.transform.position = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int atk)
    {
        hp -= atk;
        if (hp < 0) hp = 0;  //マイナス回避
    }
}
