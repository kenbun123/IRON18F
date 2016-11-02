﻿using UnityEngine;
using System.Collections;

public class CHumanUI : MonoBehaviour
{
    #region Serialize
    [SerializeField]
    GameObject[] m_humanAlive = new GameObject[3];
    [SerializeField]
    GameObject[] m_humanDead = new GameObject[3];

    [SerializeField]
    GameObject[] m_candys = new GameObject[3];
    [SerializeField]
    GameObject[] m_candyBacks = new GameObject[3];
    [SerializeField]
    GameObject m_candle = null;
    [SerializeField]
    GameObject m_candleBack = null;
    #endregion

    #region Private
    bool[] m_isAlive = new bool[3];
    bool[] m_isHaveCandy = new bool[3];
    bool m_isHaveCandle = true;
    GameObject[] m_humansUI = null;
    GameObject[] m_candysUI = null;
    GameObject m_candleUI = null;

    void SafeDestroyCandle()
    {
        if (m_candleUI != null)
            Destroy(m_candleUI);
    }
    void SefeDestroyCandy(int index)
    {
        if (m_candysUI[index] != null)
            Destroy(m_candysUI[index]);
    }
    void SefeDestroyHuman(int index)
    {
        if (m_humansUI[index] != null)
            Destroy(m_humansUI[index]);
    }
    void CreateBack()
    {
        for (int i = 0; i < 3; i++)
        { 
            GameObject temp = Instantiate(m_candyBacks[i]);
            temp.transform.parent = transform;
            temp.name = "CandyBack";
        }
        GameObject candleBack = Instantiate(m_candleBack);
        candleBack.transform.parent = transform;
        candleBack.name = "CandyBack";
    }
    void Start()
    {
        //キャンディの初期化
        
        CreateBack();
        m_humansUI = new GameObject[3];
        m_candysUI = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            m_isAlive[i] = true;
            m_isHaveCandy[i] = false;
            Alive(i);
            HaveCandy(i);
        }
    }
    #endregion

    /*!  Dead
    *!   \details	プレイヤー死んだときに読んでくれ。
                    index = 0~2
    */
    public void Dead(int index)
    {
        if (m_isAlive[index] == true)
            return;
        SefeDestroyHuman(index);
        m_humansUI[index] = Instantiate(m_humanDead[index]);
        m_humansUI[index].transform.parent = transform;
        m_humansUI[index].name = "HumanDead" + index;
        m_isAlive[index] = true;
        
    }

    /*!  Dead
    *!   \details	プレイヤー復活したときに読んでくれ。
                    index = 0~2
    */
    public void Alive(int index)
    {
        if (m_isAlive[index] == false)
            return;
        SefeDestroyHuman(index);
        m_humansUI[index] = Instantiate(m_humanAlive[index]);
        m_humansUI[index].transform.parent = transform;
        m_humansUI[index].name = "Human" + index;
        m_isAlive[index] = false;

    }
    /*!  HaveCandy
    *!   \details	キャンディ拾った時とかによんでくれ。
                    index = 0~2
    */
    public void HaveCandy(int index)
    {
        if (m_isHaveCandy[index] == true)
            return;
        SefeDestroyCandy(index);
        m_candysUI[index] = Instantiate(m_candys[index]);
        m_candysUI[index].transform.parent = transform;
        m_candysUI[index].name = "Candy" + index;
        m_isHaveCandy[index] = true;
    }
    /*!  PutCandy
    *!   \details	キャンディ投げた時とかによんでくれ。
                    index = 0~2
    */
    public void ThrowCandy(int index)
    {
        if (m_isHaveCandy[index] == false)
            return;
        SefeDestroyCandy(index);
        m_isHaveCandy[index] = false;
    }
    public void HaveCandle()
    {
        if (m_isHaveCandle == true)
            return;
        SafeDestroyCandle();
        m_candleUI = Instantiate(m_candle);
        m_candleUI.transform.parent = transform;
        m_candleUI.name = "Candke";
        m_isHaveCandle = true;
    }
    public void PutCandle()
    {
        if (m_isHaveCandle == false)
            return;
        SafeDestroyCandle();
        m_isHaveCandle = false;
    }
}
