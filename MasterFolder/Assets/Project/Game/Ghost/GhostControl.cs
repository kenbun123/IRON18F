using UnityEngine;
using System.Collections;

public class GhostControl : MonoBehaviour {

    GhostMain ghostMain;

	// Use this for initialization
	void Start () {

        ghostMain = CheckComponentNull<GhostMain>.CheckConmponentNull(gameObject, "Class GhostControl : Don't Get GhostMain");

        if (ghostMain == null)
        {
            enabled = false;
        }

	}
	
	// Update is called once per frame
	void Update () {

        if (!Input.anyKey)
        {
            ghostMain.GhostStatusMessage = GhostInfo.GhostFiniteStatus.WAITING;

        }

        Move();

        if (Input.GetKeyDown(KeyCode.X))
        {
            //攻撃
            ghostMain.GhostStatusMessage = GhostInfo.GhostFiniteStatus.ATTACK;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //ゴースト生成
            ghostMain.GhostStatusMessage = GhostInfo.GhostFiniteStatus.DUMMY;
        }
	}

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        float z = Input.GetAxisRaw("Vertical");

        // 移動する向きを求める
        Vector3 tmpDirection = new Vector3(x, 0, z);

        ghostMain.Direction= tmpDirection.normalized;

        if (ghostMain.Direction.magnitude > 0.1f)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                ghostMain.GhostStatusMessage = GhostInfo.GhostFiniteStatus.DASH;
                return;
            }

            ghostMain.GhostStatusMessage = GhostInfo.GhostFiniteStatus.WALK;
            return;
        }

        ghostMain.GhostStatusMessage = GhostInfo.GhostFiniteStatus.WAITING;
    }
}
