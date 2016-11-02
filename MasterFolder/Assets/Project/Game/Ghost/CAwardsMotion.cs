using UnityEngine;
using System.Collections;


public class CAwardsMotion : MonoBehaviour
{
    [SerializeField]
    RuntimeAnimatorController[] m_animator = new RuntimeAnimatorController[3];
	
    public void SetAnimetion(int index)
    {
        GetComponent<Animator>().runtimeAnimatorController = m_animator[index];
    }
}
