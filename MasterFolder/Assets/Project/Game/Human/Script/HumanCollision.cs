using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanCollision : MonoBehaviour {

    private const string TAG_CANDLE = "Candle";
    private const string TAG_FIRE = "Fire";
    private const string TAG_CANDY = "Candy";
    private const string TAG_STAGE = "Stage";

    private HumanMain humanMain;

    GameObject hitFireObj;

    private float elapsedTime;

    Ray ray = new Ray();

    // Use this for initialization
    void Start () {

        humanMain = CheckComponentNull<HumanMain>.CheckConmponentNull(gameObject,"Class HumanCollision : Don't Get HumanMain");

        if (humanMain == null)
        {
            enabled = false;
        }

        elapsedTime = 0f;
	}


    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        Candle(other);
        Candy(other);
    }

    void OnTriggerStay(Collider other)
    {
        Fire(other);
    }


    void Candle(Collider other)
    {
        if (other.gameObject.tag == TAG_CANDLE)
        {
            if (humanMain.CandleStock >= humanMain.MaxCandleStock) return;
            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.PICK_UP_CANDLE;

            Destroy(other.gameObject);
        }
    }

    void Candy(Collider other)
    {
        if (other.gameObject.tag == TAG_CANDY)
        {
            if (humanMain.CandyStock >= humanMain.MaxCandyStock) return;

            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.PICK_UP_CANDY;

            Destroy(other.gameObject);
        }
    }

    void Fire(Collider other)
    {
        if (other.gameObject.tag == TAG_FIRE)
        {
            if (RayCheck(other.transform.parent.transform) == false) return;

            ResusciTation();

            humanMain.Stamina = humanMain.MaxStamina;
        }
    }

    void ResusciTation()
    {
        if (humanMain.Hp <= 0)
        {
            elapsedTime += Time.deltaTime;
        }
        else {
            elapsedTime = 0f;
        }

        if (elapsedTime >= humanMain.RaiseTime)
        {
            humanMain.Hp = 1;
        }
    }

    private bool RayCheck(Transform chara)
    {
        // 壁が無いよ
        //Debug.Log( m_isNearWall );
        //if( m_isNearWall )	return true;

        var dir = chara.position;
        /* レイの方向設定 */
        dir -= transform.position;
        dir.y = 0.1f;

        
        ray.origin = new Vector3(transform.position.x,0.1f, transform.position.z);
        ray.direction = dir;

        /* レイを飛ばす */
        RaycastHit[] hits = Physics.RaycastAll(ray);

        /* レイに当たった物を検索 */
        float charaDistance = Vector3.Distance(transform.position, chara.position);

        /* 一番近い壁を検索 */
        for (int i = 0; i < hits.Length; i++)
        {
            if (TAG_STAGE == hits[i].transform.tag)
            {
                return true;
            }

        }
        return false;

    }
}
