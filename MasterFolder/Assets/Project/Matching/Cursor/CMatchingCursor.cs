using UnityEngine;
using System.Collections;

public class CMatchingCursor : MonoBehaviour
{
    public  bool m_isGhostSelect = false;

    bool m_syncSelect;

    public bool IsSetId = false;
    Coroutine m_moveCoroutine =null;

    [SerializeField]
    
    Vector3[] m_ghostSelectPos = new Vector3[4];
    [SerializeField]
    Vector3[] m_humanSelectPos = new Vector3[4];
    [SerializeField]

    public int m_id = 999;
    float m_sec = 0.3f;
    FEase SpringLeap = CEase.SPRING;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }
	// Update is called once per frame
	void Update ()
    {
        if (IsSetId && transform.position == new Vector3(0,0,0))
        {

            transform.position = m_humanSelectPos[m_id];
            m_moveCoroutine = StartCoroutine(Move(m_sec));
        }

        if (m_moveCoroutine == null)
        {

            if (m_syncSelect != m_isGhostSelect)
            {
                m_syncSelect = m_isGhostSelect;
                m_moveCoroutine = StartCoroutine(Move(m_sec));
            }


        }
	}


    public void SetId(int value)
    {
        m_id = value;
        IsSetId = true;
    }

    IEnumerator Move(float sec)
    {
        Vector3 target,old;
        if(m_isGhostSelect)
        {
            target = m_ghostSelectPos[m_id];
            old = m_humanSelectPos[m_id];
        }
        else
        {
            target = m_humanSelectPos[m_id];
            old = m_ghostSelectPos[m_id];
        }
        for (float i = 0; i < sec; i += Time.deltaTime)
        {
            yield return 0;
            transform.SetX( SpringLeap(target.x, old.x, i / sec));
            transform.SetY( SpringLeap(target.y, old.y, i / sec));
        }
        SpringLeap(m_ghostSelectPos[m_id].x, m_humanSelectPos[m_id].x, 1);
        m_moveCoroutine = null;
    }
}
