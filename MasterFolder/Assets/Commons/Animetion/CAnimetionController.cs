using UnityEngine;
using System.Collections;

public class CAnimetionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public bool IsMotionEnd(Animator anim, string name)
    {
        bool isname = anim.GetCurrentAnimatorStateInfo(0).IsName(name);

        if (!isname) return false;

        bool istime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;

        if (istime)
        {
            return true;
        }

        return false;
    }
}
