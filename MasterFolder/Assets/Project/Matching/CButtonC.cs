using UnityEngine;
using System.Collections;

public class CButtonC : MonoBehaviour {

	
	// Update is called once per frame
	void Update (){
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.S)||
            Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            Destroy(gameObject);
	
	}
}
