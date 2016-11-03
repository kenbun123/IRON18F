//using UnityEngine;
//using System.Collections;

//public class SGhostScore
//{
//    public int knockOut;  //気絶させた数
//    public int hitCandy;  //キャンディにあたった数
//    //float walkDistance; //歩いた距離
//    public float score;
//}
//public class SHumanScore
//{
//    public int setCandle;      //キャンドルを設置した数
//    public int resuscitation;  //仲間を生き返した数
//    public int setAlterCandle; //燭台にキャンドルを設置した数
//    public float score;
//    //float walkDistance; //歩いた距離
//}

//public class CGameScore : CSingleton<CGameScore>
//{
//    SGhostScore m_ghost = new SGhostScore();        //ゴーストのスコア
//    SHumanScore[] m_humans = new  SHumanScore[3];   //人間のスコア

//    override protected void Awake()
//    {
//        ResetAll();
//    }
//	public void ResetAll()
//    {
//        if (m_humans[0] == null)
//            for (int i = 0; i < 3; i++)
//            {
//                m_humans[i] = new SHumanScore();
//            }
//        m_ghost.knockOut = 0;
//        m_ghost.hitCandy = 0;
//        m_ghost.score = 0;
//        for (int i = 0; i < 3; i++)
//        {
//            m_humans[i].resuscitation = 0;
//            m_humans[i].setAlterCandle = 0;
//            m_humans[i].setCandle = 0;
//            m_humans[i].score = 0;
//        }
//    }
//    public void WinGhost()
//    {
//        m_ghost.score = 5 + Random.Range(0.0f, 5.0f);
//        for (int i = 0; i < 3; i++)
//            m_humans[i].score = Random.Range(0.0f, 5.0f);
//    }
//    public void WinHuman()
//    {
//        m_ghost.score =Random.Range(0.0f, 5.0f);
//        for (int i = 0; i < 3; i++)
//            m_humans[i].score = 5 + Random.Range(0.0f, 5.0f);
//    }
//    /*! SetHumanScore
//    *!   \details	ヒューマンのスコア設定
//    *!
//    */
//    public void SetHumanScore(CHuman human)
//    {
//        m_humans[human.HumanID].resuscitation = human.ShumanScore.resuscitation;
//        m_humans[human.HumanID].setAlterCandle = human.ShumanScore.setAlterCandle;
//        m_humans[human.HumanID].setCandle = human.ShumanScore.setCandle;
//    }

//    /*!  GetHumanScore
//    *!   \details	ヒューマンのスコア取得 ※0.00~ 10.00で収まるように
//    *!
//    *!   \return	ヒューマンのスコア
//    */
//    public float GetHumanScore(int index)
//    {
//        //いい感じの計算式おねがいします^p^
////        float score = 
////            m_humans[index].resuscitation +
////            m_humans[index].setAlterCandle +
////            m_humans[index].setCandle ;
//        return m_humans[index].score;
//    }


//    /*! SetGhostScore
//    *!   \details	ゴーストのスコア設定
//    *!
//    */
//    public void SetGhostScore(GhostMain ghost)
//    {
//        m_ghost.hitCandy = ghost.GhostScore.hitCandy;
//        m_ghost.knockOut = ghost.GhostScore.knockOut;
//    }


//    public int GetWinerIndex()
//    {
//        int ret = 3;
//        float max = GetGhostScore();
//        for (int i = 0; i < 3; i++)
//            if (max < GetHumanScore(i))
//            {
//                max = GetHumanScore(i);
//                ret = i;
//            }
//        return ret;
//    }

//    public int GetLoseIndex()
//    {
//        int ret = 3;
//        float max = GetGhostScore();
//        for (int i = 0; i < 3; i++)
//            if (max >= GetHumanScore(i))
//            {
//                max = GetHumanScore(i);
//                ret = i;
//            }
//        return ret;
//    }

//    /*!  GetGhostScore
//    *!   \details	ゴーストのスコア取得　※0.00~ 10.00で収まるように
//    *!
//    *!   \return	ゴーストのスコア
//    */
//    public float GetGhostScore()
//    {
//        //いい感じの計算式おねがいします^p^
//        float score =
//            m_ghost.hitCandy +
//            m_ghost.knockOut;
//        return m_ghost.score;
//    }
//}
