using UnityEngine;
using System.Collections;

public class HumanFearCollider : MonoBehaviour {

    private HumanMain humanMain;

    float distance;



    // Use this for initialization
    void Start () {

        humanMain = transform.parent.GetComponent<HumanMain>();
	}
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ghost")
        {
            humanMain.isFear = true;
            float tmp = Vector3.Distance(other.transform.position, transform.position);
            if (tmp < distance)
            {
                distance = tmp;
            }


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ghost")
        {
            humanMain.isFear = false;
        }
    }
}
