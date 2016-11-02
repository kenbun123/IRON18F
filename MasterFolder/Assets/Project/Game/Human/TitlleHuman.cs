using UnityEngine;
using System.Collections;

public class TitlleHuman : MonoBehaviour {

    public int HumanID;
    public Material[] HumanMaterial;
	// Use this for initialization
	void Start () {
        ColorChange();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void ColorChange()
    {
       
        GameObject tmp_obj = null;
        foreach (Transform child in this.transform)
        {
            if (child.name == "polySurface12")
            {
                tmp_obj = child.gameObject;
            }
        }
        if (tmp_obj != null)
        {
            switch (this.HumanID)
            {
                case 0:
                    ChildSet(tmp_obj.transform, HumanMaterial[0]);
                    break;
                case 1:
                    ChildSet(tmp_obj.transform, HumanMaterial[0]);
                    break;
                case 2:
                    ChildSet(tmp_obj.transform, HumanMaterial[1]);
                    break;
                case 3:
                    ChildSet(tmp_obj.transform, HumanMaterial[2]);
                    break;
            }
        }
        

    }

    public void ChildSet(Transform tr, Material mat)
    {
        foreach (Transform child in tr)
        {
            child.GetComponent<SkinnedMeshRenderer>().material = mat;
        }
    }
}
