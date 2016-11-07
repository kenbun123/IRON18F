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
        if (other.gameObject.tag == TAG_CANDLE)
        {
            if (humanMain.CandleStock >= humanMain.MaxCandleStock) return;
            humanMain.HumanStatusMessage = HumanInfo.HumanFiniteStatus.PICK_UP_CANDLE;
            humanMain.CandleStock += 1;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == TAG_CANDY)
        {
            
        }
    }
}
