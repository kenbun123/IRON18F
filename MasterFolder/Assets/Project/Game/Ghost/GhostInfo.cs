using UnityEngine;
using System.Collections;

public class GhostInfo : MonoBehaviour {

    public enum GhostFiniteStatus {
        WAITING = 0,
        WALK,
        DASH,
        SLOW,
        ATTACK,
        STAN,
        DUMMY
    }

    [SerializeField]
    [Header("通常移動速度")]
    protected float normalSpeed;

    [SerializeField]
    [Header("ダッシュ速度")]
    protected float dashSpeed;

    [SerializeField]
    [Header("スロー速度")]
    protected float slowSpeed;

    [SerializeField]
    [Header("ダミーの最大数")]
    protected int maxDummyGhostNum;


    public float NormalSpeed
    {
        get { return normalSpeed; }
    }
    public float DashSpeed
    {
        get { return dashSpeed; }
    }
    public float SlowSpeed {
        get { return slowSpeed;}
    }
    public int MaxDummyGhostNum
    {
        get { return maxDummyGhostNum; }
    }
}
