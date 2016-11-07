using UnityEngine;
using System.Collections;

public static class CAnimetionController  {




    public  static bool IsMotionEnd(Animator anim, string name)
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
