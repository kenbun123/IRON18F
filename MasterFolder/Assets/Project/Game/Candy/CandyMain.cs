using UnityEngine;
using System.Collections;

public class CandyMain : MonoBehaviour {


    private const string TAG_STAGE = "Stage";
    private const float SPEED_CORRECION = 1.0F;


    // ===== メンバ変数 =====
    /* キャンディ用変数 */
    [SerializeField][Header("スピード")]
    private float speed;       // スピード

    [SerializeField][Header("最大距離")]
    private float maxDistance;  // 最大距離


    private float distance;       // 残りの移動距離
    private Vector3 vector;       // 発射方向
    private bool isShot;
    private SphereCollider coll;

    public Vector3 Direction {
        set { vector = value; }
    }

    // Use this for initialization
    void Start ()
    {
        
        isShot = true;
        distance = maxDistance;

        coll = GetComponent<SphereCollider>();
        coll.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (distance <= 0)
        {
            isShot = false;
            coll.enabled = true;
        }
        if (isShot == false) return;

        Vector3 addPos = vector * speed * Time.deltaTime;
        transform.position += addPos;
        distance -= Vector3.Distance(Vector3.zero, addPos);

    }

    void OnTriggerEnter(Collider value)
    {
        if (value.gameObject.tag == "Stage")
        {
            isShot = false;
        }
    }
}
