using UnityEngine;
using System.Collections;

public class HumanInfo : MonoBehaviour {

    public enum HumanFiniteStatus
    {
        WAITING = 0,
        WALK,
        DASH,
        PICK_UP_CANDLE,
        PUT_CANDLE,
        PICK_UP_CANDY,
        USE_CANDY,
        DEATH,
        RESCUE,
        ACTION
    };

    [SerializeField][Header("通常移動速度")]
    protected float normalSpeed;

    [SerializeField][Header("ダッシュ速度")]
    protected float dashSpeed;

    [SerializeField][Header("Hp")]
    protected int hp;

    [SerializeField][Header("最大キャンディ所持数")]
    protected float MaxCandyNum;

    [SerializeField][Header("最大スタミナ")]
    protected float MaxStamina;

}
