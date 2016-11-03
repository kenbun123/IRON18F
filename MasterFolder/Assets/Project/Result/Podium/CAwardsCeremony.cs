//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Networking;
//public class CAwardsCeremony : MonoBehaviour
//{
//    [SerializeField]
//    CPodium[] m_podiums =new CPodium[4];

//    [SerializeField]
//    List<CCeremonyEffect> m_effects = null;
    
//    [SerializeField]
//    float m_podiumUpperSec = 10;

//    GameObject m_LobbyManager;
        

//	// Use this for initialization
//	void Start ()
//    {

//        m_LobbyManager = GameObject.Find("LobbyManager").gameObject;

//        m_podiums[0].StartTween(m_podiumUpperSec, CGameScore.Instance.GetHumanScore(0));
//        m_podiums[1].StartTween(m_podiumUpperSec, CGameScore.Instance.GetHumanScore(1));
//        m_podiums[2].StartTween(m_podiumUpperSec, CGameScore.Instance.GetHumanScore(2));
//        m_podiums[3].StartTween(m_podiumUpperSec, CGameScore.Instance.GetGhostScore());
//        for (int i = 0; i < 4;i++ )
//        {
//           if(CGameScore.Instance.GetWinerIndex() ==i)
//               m_podiums[i].transform.GetChild(0).GetComponent<CAwardsMotion>().SetAnimetion(2);
//           else if (CGameScore.Instance.GetLoseIndex() == i)
//               m_podiums[i].transform.GetChild(0).GetComponent<CAwardsMotion>().SetAnimetion(1);
//           else
//               m_podiums[i].transform.GetChild(0).GetComponent<CAwardsMotion>().SetAnimetion(0);
//        }
//        StartCoroutine(EffectEvent());
//    }


//    //効果にイベントを送る
//    IEnumerator EffectEvent()
//    {
//        CreateEffects();    //効果作成
//        CSoundManager.Instance.PlaySE(EAudioList.SE_AwardDrumroll);
//        yield return new WaitForSeconds(m_podiumUpperSec);
//        //優勝者を各効果に通知する
//        for (int i = 0; i < m_effects.Count; i++)
//            m_effects[i].Decision(CGameScore.Instance.GetWinerIndex());
//        CSoundManager.Instance.PlaySE(EAudioList.SE_AwardAnnouncement);
//        //CSoundManager.Instance.PlaySE(EAudioList.SE_WinerDecision1);
//        CSoundManager.Instance.PlaySE(EAudioList.SE_WinerDecision2);
//        StartCoroutine(GameEnd(8));
//    }
//    IEnumerator GameEnd(float sec)
//    {
//        yield return new WaitForSeconds(sec);

//        Destroy(m_LobbyManager);
//        NetworkManager.Shutdown();
//        FadeManager.Instance.LoadLevel(SCENE_RAVEL.TITLE, 0.5f, null,SceneManager.LoadScene);
//    }
//    //効果作成
//    void CreateEffects()
//    {
//        for (int i = 0; i < m_effects.Count; i++)
//        {
//            m_effects[i] = Instantiate(m_effects[i]);
//            m_effects[i].SetCeremony(this, m_podiums);
//            m_effects[i].name = "Effect" + i;
//            m_effects[i].transform.parent = transform;
//        }
//    }
    
//	// Update is called once per frame
//	void Update ()
//    {
	
//	}
//}
