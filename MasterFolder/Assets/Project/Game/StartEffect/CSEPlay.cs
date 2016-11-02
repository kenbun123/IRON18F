using UnityEngine;
using System.Collections;

public class CSEPlay : MonoBehaviour {

    [SerializeField]
    EAudioList m_playSE;
	// Use this for initialization
	void Start () {
        CSoundManager.Instance.PlaySE(m_playSE);
	}
	
}
