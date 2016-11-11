using UnityEngine;
using System.Collections;

public class HumanControl : MonoBehaviour {

    private HumanMain humanMain;
	// Use this for initialization
	void Start () {

        humanMain = gameObject.GetComponent<HumanMain>();
	}
	
	// Update is called once per frame
	void Update () {

        if (humanMain == null)
        {
            Debug.Log("Class HumanControl Don't Get humanMain");
            enabled = false;
            return;
        }
        if (!Input.anyKey) {
            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.WAITING;
            return;
        }

        Move();

        //ロウソクを置く
        if (Input.GetKeyDown(KeyCode.X))
        {
            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.PUT_CANDLE;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.USE_CANDY;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.ACTION;
        }
        
	}

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        float z = Input.GetAxisRaw("Vertical");

        // 移動する向きを求める
        Vector3 tmpDirection = new Vector3(x, 0, z);

        humanMain.MoveDirection = tmpDirection.normalized;

        if (humanMain.MoveDirection.magnitude > 0.1f) { 

            if (Input.GetKey(KeyCode.LeftShift))
            {
                humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.DASH;
                return;
            }

            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.WALK;
            return;
        }

        humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.WAITING;


    }


}
