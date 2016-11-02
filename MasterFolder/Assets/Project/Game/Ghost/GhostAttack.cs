using UnityEngine;
using System.Collections;

public class GhostAttack : MonoBehaviour
{

    GhostMain m_ghost;

    // Use this for initialization
    void Start()
    {
        
        m_ghost = gameObject.transform.parent.gameObject.GetComponent<GhostMain>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Human")
        {
            if (m_ghost.IsAttack)
            {
                m_ghost.m_nowAttackHuman = other.gameObject;
                //プレイヤーへの攻撃
                if (!other.GetComponent<CHuman>().Doa())
                {
                    //CSoundManager.Instance.PlaySE(EAudioList.SE_GhostLaugh);
                    m_ghost.GhostScore.knockOut++;
                }
                
            }
        }
    
        
    }

}