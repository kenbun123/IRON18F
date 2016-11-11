using UnityEngine;
using System.Collections;

public class GhostCollider : MonoBehaviour {


    GhostMain main;
	// Use this for initialization
	void Start () {
        main = CheckComponentNull<GhostMain>.CheckConmponentNull(gameObject,GetType().FullName + ":Don't Get GhostMain");
        if (main == null)
        {
            enabled = false;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        
    }
}
