using UnityEngine;
using System.Collections;

public class CGhostUI : MonoBehaviour {

    #region Serialize
    [SerializeField]
    GameObject[] m_DMGhost = new GameObject[3];
    [SerializeField]
    GameObject[] m_DMGhostBack = new GameObject[3];

    [SerializeField]
    GameObject m_ghostOn;
    [SerializeField]
    GameObject m_ghostOff;

    [SerializeField]
    GameObject m_ghostOnObj;
    [SerializeField]
    GameObject[] m_DMGhostObj = new GameObject[3];
    #endregion

    #region Private
    bool m_isOff = true;
    bool []m_isHaveDMGhost = new bool[3];
    GameObject m_ghostUI = null;
    GameObject[] m_dmghostUI = null;
    #endregion

   void Create()
    {
       for(int i=0;i<3;i++)
       {
           HaveDMGhost(i);
           GameObject tempDM = Instantiate(m_DMGhostBack[i]);
           tempDM.transform.parent = transform;
       }

       GameObject temp = Instantiate(m_ghostOff);
       temp.transform.parent = transform;
    }

   public void SetDMGhost(int num)
   {
       if (m_isHaveDMGhost[num] == false)
           return;
       if (m_DMGhostObj[num] != null)
           Destroy(m_DMGhostObj[num]);
       m_isHaveDMGhost[num] = false;
   }
   public void HaveDMGhost(int num)
   {
       if (m_isHaveDMGhost[num] == true)
           return;
       m_DMGhostObj[num] = Instantiate(m_DMGhost[num]);
       m_DMGhostObj[num].transform.parent = transform;
       m_isHaveDMGhost[num] = true;
   }

   public void GhostToOff()
   {
       if (m_isOff==true)
           return;
       if (m_ghostOnObj != null)
           Destroy(m_ghostOnObj);
       m_isOff = true;
   }

   public void GhostToOn()
   {
       if (m_isOff == false)
           return;
       m_ghostOnObj = Instantiate(m_ghostOn);
       m_ghostOnObj.transform.parent = transform;
       m_isOff = false;
   }

   void Start()
   {
       //キャンディの初期化
       Create();
      for(int i=0;i<3;i++)
      {
          m_isHaveDMGhost[i] = true;
      }
   }
}
