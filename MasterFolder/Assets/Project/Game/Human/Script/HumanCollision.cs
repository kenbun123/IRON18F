using UnityEngine;
using System.Collections;

public class HumanCollision : MonoBehaviour {

    private const string TAG_CANDLE = "Candle";
    private const string TAG_FIRE = "Fire";
    private const string TAG_CANDY = "Candy";

    private HumanMain humanMain;

    
    // Use this for initialization
    void Start () {

        humanMain = CheckComponentNull<HumanMain>.CheckConmponentNull(this,"Class HumanCollision : Don't Get HumanMain");

        if (humanMain == null)
        {
            enabled = false;
        }
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
            ResusciTation();

            humanMain.Stamina = humanMain.MaxStamina;
        }
    }

    void ResusciTation()
    {
        if (humanMain.Hp <= 0)
        {
            
        }
    }


}
