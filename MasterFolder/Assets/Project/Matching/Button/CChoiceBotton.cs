using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CChoiceBotton : MonoBehaviour {

    private GameObject m_leftobj;
    private GameObject m_rightobj;

    private GameObject UIobj;
    private GameObject localCursorobj;

    public bool choice;
	// Use this for initialization
	void Start () {

        UIobj = GameObject.Find("UI");
        //localCursorobj = GameObject.Find("localCursor");

        UIobj.SetActive(false);
        //localCursorobj.SetActive(false);

        m_leftobj = GameObject.Find("ButtonLeft");
        m_rightobj = GameObject.Find("ButtonRight");

        m_leftobj.GetComponent<Button>().onClick.AddListener(OnLeftButtonClick);
        m_rightobj.GetComponent<Button>().onClick.AddListener(OnRightButtonClick);

        
	}
    void Update()
    {
        if (choice)
        {
            UIobj.SetActive(true);
            //localCursorobj.SetActive(true);
            this.gameObject.SetActive(false);
            Debug.Log("false");
        }
    }
    void OnLeftButtonClick()
    {
        Debug.Log("左のボタン押された時の処理");

        choice = true;
    }
    void OnRightButtonClick()
    {
        Debug.Log("右のボタン押された時の処理");

        choice = true;
    }
}
